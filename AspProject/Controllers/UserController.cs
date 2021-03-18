using AspProject_Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspProject.Controllers
{
    public class UserController:Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View(_userService.GetAllUsers());
        }
    }
}
