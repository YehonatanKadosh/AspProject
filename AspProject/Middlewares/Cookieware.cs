using AspProject_Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Cookieware
    {
        private readonly RequestDelegate _next;

        public Cookieware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, IUserService Userservice)
        {
            if (httpContext.Request.Cookies["AspProjectCookie"] != null)
            {
                string[] UsernamePassword = httpContext.Request.Cookies["AspProjectCookie"].Split(',');
                httpContext.Items.Add("User", Userservice.Get_User_Details(UsernamePassword[0], UsernamePassword[1]));
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Cookieware>();
        }
    }
}
