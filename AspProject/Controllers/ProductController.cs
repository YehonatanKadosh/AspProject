using AspProject_Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspProject.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
