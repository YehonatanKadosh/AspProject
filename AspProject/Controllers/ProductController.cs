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
                    using (var ms = new MemoryStream())
                    {
                        Images[i].CopyTo(ms);
                        switch (i)
                        {
                            case 0:
                                product.Picture1 = ms.ToArray();
                                break;
                            case 1:
                                product.Picture2 = ms.ToArray();
                                break;
                            case 2:
                                product.Picture3 = ms.ToArray();
                                break;
                        }
                    }
                }
            }
            product.State = ProductState.UnSold;
            string[] UsernamePassword = HttpContext.Request.Cookies["AspProjectCookie"].Split(',');
            product.Owner = product.User = _userService.GetUser(UsernamePassword[0], UsernamePassword[1]);
            product.LastModified = product.Date = DateTime.Now;
            _productService.AddProduct(product);
            return RedirectToAction("WelcomePage", "Master");
        }
       
        public IActionResult addToCart(int id)
        {
            return View();
        }
        public IActionResult ProductDetails(int id)
        {
            return View(_productService.getProductByID(id));
        }
    }
}
