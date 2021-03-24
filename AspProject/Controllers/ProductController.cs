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
        private IHostingEnvironment Environment;

        public ProductController(IProductService productService, IUserService userService, IHostingEnvironment environment)
        {
            _productService = productService;
            _userService = userService;
            Environment = environment;
        }
        public IActionResult New_Advert()
        {
            if (HttpContext.Request.Cookies["AspProjectCookie"] != null)
            {
                string[] UsernamePassword = HttpContext.Request.Cookies["AspProjectCookie"].Split(',');
                ViewData["User"] = _userService.Get_User_Details(UsernamePassword[0], UsernamePassword[1]);
            }
            return View();
        }
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public IActionResult AddProduct(Product product, List<IFormFile> Images)
        {
            for (int i = 0; i < 3; i++)
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
            return View();
        }
    }
}
