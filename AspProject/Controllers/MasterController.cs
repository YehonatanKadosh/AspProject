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
        public IUserService Userservice { get; }
           
        public MasterController(IUserService userservice)
        {
            Userservice = userservice;
        }
        public IActionResult WelcomePage()
        {
            if(HttpContext.Request.Cookies["AspProjectCookie"]!=null)
            {
                string[] UsernamePassword = HttpContext.Request.Cookies["AspProjectCookie"].Split(',');
                ViewData["User"] = Userservice.Get_User_Details(UsernamePassword[0], UsernamePassword[1]);
            }
            return View();
        }
    }
}
