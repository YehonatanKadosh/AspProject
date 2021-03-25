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
        private IProductService ProductService;

        private IUserService Userservice;
           
        public MasterController(IUserService userService, IProductService productService)
        {
            Userservice = userService;
            ProductService = productService;
        }
        public IActionResult WelcomePage()
        {
            return View(ProductService.GetAllAvailable());
        }
    }
}
