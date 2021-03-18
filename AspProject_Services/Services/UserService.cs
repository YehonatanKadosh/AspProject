using AspProject_DataBase.Context;
using AspProject_Entities.Models;
using AspProject_Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AspProject_Services.Services
{
    public class UserService : IUserService
    {
        private DBContext Context;

        public UserService(DBContext context)
        {
            Context = context;
        }
        public IEnumerable<User> GetAllUsers() => Context.Users;
    }
}
