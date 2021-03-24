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
            return View();
        }
    }
}
