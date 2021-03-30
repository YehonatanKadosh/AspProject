using AspProject_Entities.Models;
using AspProject_Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspProject.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        private IProductService _productService;

        public UserController(IUserService userService, IProductService productService)
        {
            _userService = userService;
            _productService = productService;
        }
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult SignIn(SignInModel signInModel)
        {
            User user = _userService.GetUser(signInModel.Username, signInModel.Password);
            if (user != null)
            {
                HttpContext.Response.Cookies.Append("AspProjectCookie", $"{signInModel.Username},{signInModel.Password}", new CookieOptions() { Expires = DateTime.Now.AddDays(3) });
                if (HttpContext.Request.Cookies.ContainsKey("AspProjectGuestCart"))
                {
                    HttpContext.Request.Cookies["AspProjectGuestCart"].Split(',').ToList().ForEach(idstring => _productService.AddProductToCart(int.Parse(idstring), user)); ;
                    HttpContext.Response.Cookies.Delete("AspProjectGuestCart");
                }
            }
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
        public IActionResult PasswordCheck(string username, string password)
        {
            if (_userService.CheckIfExists(username))
                if (_userService.CheckIfPasswordMatch(username, password))
                    return StatusCode(200);
            return StatusCode(404);
        }
        public IActionResult AddUser(User user)
        {
            if (!ModelState.IsValid)
                return View("SignUp", user);
            else
            {
                _userService.AddUser(user);
                HttpContext.Response.Cookies.Append("AspProjectCookie", $"{user.UserName},{user.Password}", new CookieOptions() { Expires = DateTime.Now.AddDays(3) });
                if (HttpContext.Request.Cookies.ContainsKey("AspProjectGuestCart"))
                {
                    HttpContext.Request.Cookies["AspProjectGuestCart"].Split(',').ToList().ForEach(idstring => _productService.AddProductToCart(int.Parse(idstring), user)); ;
                    HttpContext.Response.Cookies.Delete("AspProjectGuestCart");
                }
            }
            return RedirectToAction("WelcomePage", "Master");
        }
    }
}
