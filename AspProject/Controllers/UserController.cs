using AspProject_Entities.Models;
using AspProject_Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AspProject.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult SignIn(string username, string password)
        {
            string user = _userService.Get_User_Details(username, password);
            if (user != null)
                HttpContext.Response.Cookies.Append("AspProjectCookie", $"{username},{password}", new CookieOptions() { Expires = (DateTime.Now).AddDays(3) });
            return RedirectToAction("WelcomePage", "Master");
        }
        public IActionResult SignOut()
        {
            HttpContext.Response.Cookies.Delete("AspProjectCookie");
            return RedirectToAction("WelcomePage", "Master");
        }
        [HttpPost]
        public IActionResult UsernameCheck(string username)
        {
            if (_userService.CheckIfExists(username))
                return StatusCode(200);
            else return StatusCode(404);
        }
        public IActionResult AddUser(User user)
        {
            if (!ModelState.IsValid)
                return View("SignUp", user);
            else
            {
                _userService.AddUser(user);
                HttpContext.Response.Cookies.Append("AspProjectCookie", $"{user.UserName},{user.Password}", new CookieOptions() { Expires = DateTime.Now.AddDays(3) });
            }
            return RedirectToAction("WelcomePage", "Master");
        }
    }
}
