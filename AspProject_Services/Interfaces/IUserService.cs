﻿using AspProject_Entities.Models;
using System.Collections.Generic;

namespace AspProject_Services.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<User> GetAllUsers();
        bool CheckIfExists(string username);
        void AddUser(User user);
        string Get_User_Details(string username, string password);
    }
}
