using AspProject_Entities.Models;
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
        private readonly IProductService _productService;

        public MasterController(IProductService productService) => _productService = productService;
        public IActionResult WelcomePage()
        {
            if (TempData.ContainsKey("SortingType"))
            {
                List<Product> ProductList;
                if (HttpContext.Request.Cookies.ContainsKey("AspProjectGuestCart"))
                {
                    List<int> ProductsInAnnonymusCart = new List<int>();
                    HttpContext.Request.Cookies["AspProjectGuestCart"].Split(',').ToList().ForEach(idstring => ProductsInAnnonymusCart.Add(int.Parse(idstring)));
                    ProductList = _productService.GetAllAvailableProducts(ProductsInAnnonymusCart).ToList();
                }
                else
                    ProductList = _productService.GetAllAvailableProducts().ToList();

                if ((string)TempData["SortingType"] == "Title")
                    ProductList.Sort(delegate (Product a, Product b) { return a.Title.CompareTo(b.Title); });
                else
                    ProductList.Sort(delegate (Product a, Product b) { return a.Date.CompareTo(b.Date); });

                return View(ProductList);
            }
            if (HttpContext.Request.Cookies.ContainsKey("AspProjectGuestCart"))
            {
                List<int> ProductsInAnnonymusCart = new List<int>();
                HttpContext.Request.Cookies["AspProjectGuestCart"].Split(',').ToList().ForEach(idstring => ProductsInAnnonymusCart.Add(int.Parse(idstring)));
                return View(_productService.GetAllAvailableProducts(ProductsInAnnonymusCart));
            }
            else return View(_productService.GetAllAvailableProducts());
        }
        public IActionResult AboutUs() => View();
    }
}
