using AspProject_Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.Controllers
{
    public class MasterController : Controller
    {
        private readonly IProductService ProductService;

        public MasterController(IProductService productService) => ProductService = productService;
        public IActionResult WelcomePage()
        {
            if (HttpContext.Request.Cookies.ContainsKey("AspProjectGuestCart"))
            {
                List<int> ProductsInAnnonymusCart = new List<int>();
                HttpContext.Request.Cookies["AspProjectGuestCart"].Split(',').ToList().ForEach(idstring => ProductsInAnnonymusCart.Add(int.Parse(idstring)));
                return View(ProductService.GetAllAvailableProducts(ProductsInAnnonymusCart));
            }
            return View(ProductService.GetAllAvailableProducts());
        }
        public IActionResult AboutUs() => View();
    }
}
