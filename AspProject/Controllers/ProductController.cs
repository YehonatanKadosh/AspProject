using AspProject_Entities.Enums;
using AspProject_Entities.Models;
using AspProject_Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IUserService _userService;

        public ProductController(IProductService productService, IUserService userService)
        {
            _productService = productService;
            _userService = userService;
        }
        public IActionResult New_Advert() => View();
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public IActionResult AddProduct(Product product, List<IFormFile> Images)
        {
            if (!ModelState.IsValid)
                return View("New_Advert", product);
            for (int i = 0; i < Images.Count; i++)
            {
                if (Images[i].Length > 0)
                {
                    using MemoryStream ms = new MemoryStream();
                    Images[i].CopyTo(ms);
                    product.GetType().GetProperty($"Picture{i + 1}").SetValue(product, ms.ToArray());
                }
            }
            product.State = ProductState.UnSold;
            string[] UsernamePassword = HttpContext.Request.Cookies["AspProjectCookie"].Split(',');
            product.Seler = product.Buyer = _userService.GetUser(UsernamePassword[0], UsernamePassword[1]);
            product.LastModified = product.Date = DateTime.Now;
            _productService.AddProduct(product);
            return RedirectToAction("WelcomePage", "Master");
        }
        public IActionResult AddToCart(int id)
        {
            if (HttpContext.Request.Cookies.ContainsKey("AspProjectCookie"))
            {
                string[] UsernamePassword = HttpContext.Request.Cookies["AspProjectCookie"].Split(',');
                _productService.AddProductToCart(id, _userService.GetUser(UsernamePassword[0], UsernamePassword[1]));
            }
            else if (HttpContext.Request.Cookies.ContainsKey("AspProjectGuestCart"))
                HttpContext.Response.Cookies.Append("AspProjectGuestCart", HttpContext.Request.Cookies["AspProjectGuestCart"] + $",{id}", new CookieOptions() { Expires = DateTime.Now.AddDays(7) });
            else
                HttpContext.Response.Cookies.Append("AspProjectGuestCart", $"{id}", new CookieOptions() { Expires = DateTime.Now.AddDays(7) });
            return RedirectToAction("WelcomePage", "Master");
        }
        public IActionResult ProductDetails(int id)
        {
            return View(_productService.GetProductByID(id));
        }
        public IActionResult RemoveFromCart(int id)
        {
            if (HttpContext.Request.Cookies.ContainsKey("AspProjectCookie"))
            {
                string[] UsernamePassword = HttpContext.Request.Cookies["AspProjectCookie"].Split(',');
                _productService.RemoveFromCart(id, _userService.GetUser(UsernamePassword[0], UsernamePassword[1]));
                return View("Cart", _productService.GetCart(_userService.GetUser(UsernamePassword[0], UsernamePassword[1])));
            }
            else
            {
                List<string> AnonymusCartItems = HttpContext.Request.Cookies["AspProjectGuestCart"].Split(',').ToList();
                AnonymusCartItems.Remove(id.ToString());
                if (AnonymusCartItems.Count == 0)
                {
                    HttpContext.Response.Cookies.Delete("AspProjectGuestCart");
                    return RedirectToAction("WelcomePage", "Master");
                }
                else
                {
                    HttpContext.Response.Cookies.Append("AspProjectGuestCart", string.Join(',', AnonymusCartItems), new CookieOptions() { Expires = DateTime.Now.AddDays(7) });
                    List<int> ProductIDs = new List<int>();
                    AnonymusCartItems.ForEach(idstring => ProductIDs.Add(int.Parse(idstring)));
                    return View("Cart", _productService.GetCart(ProductIDs));
                }
            }
        }
        public IActionResult Cart()
        {
            if (HttpContext.Request.Cookies.ContainsKey("AspProjectCookie"))
            {
                string[] UsernamePassword = HttpContext.Request.Cookies["AspProjectCookie"].Split(',');
                return View(_productService.GetCart(_userService.GetUser(UsernamePassword[0], UsernamePassword[1])));
            }
            else
            {
                if (HttpContext.Request.Cookies.ContainsKey("AspProjectGuestCart"))
                {
                    List<int> ProductIDs = new List<int>();
                    HttpContext.Request.Cookies["AspProjectGuestCart"].Split(',').ToList().ForEach(idstring => ProductIDs.Add(int.Parse(idstring))); ;
                    return View(_productService.GetCart(ProductIDs));
                }
                else
                {
                    return View(Enumerable.Empty<Product>());
                }
            }
        }
        public async Task<IActionResult> Purchase()// Add sold item filtering-----------------------------------------
        {
            if (HttpContext.Request.Cookies.ContainsKey("AspProjectCookie"))
            {
                string[] UsernamePassword = HttpContext.Request.Cookies["AspProjectCookie"].Split(',');
                await _productService.Purchase(_userService.GetUser(UsernamePassword[0], UsernamePassword[1]));
            }
            else if (HttpContext.Request.Cookies.ContainsKey("AspProjectGuestCart"))
            {
                List<int> ProductIDs = new List<int>();
                HttpContext.Request.Cookies["AspProjectGuestCart"].Split(',').ToList().ForEach(idstring => ProductIDs.Add(int.Parse(idstring))); ;
                await _productService.Purchase(ProductIDs);
            }
            return RedirectToAction("Thanks", "Product");
        }
        public IActionResult Thanks()
        {
            return View();
        }
    }
}
