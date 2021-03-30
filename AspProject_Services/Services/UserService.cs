using AspProject_DataBase.Context;
using AspProject_Entities.Models;
using AspProject_Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace AspProject_Services.Services
{
    public class UserService : IUserService
    {
        private DBContext Context;

        public UserService(DBContext context)
        {
            Context = context;
        }

        public void AddUser(User user)
        {
            if (Context.Users.FirstOrDefault(_user => _user.UserName == user.UserName) == null)
            {// if doesnt exists// if someone sent the same form accidentally
                Context.Users.Add(user);
                Context.SaveChanges();
            }
        }

        public bool CheckIfExists(string username)
        => Context.Users.Where(user => user.UserName == username).FirstOrDefault() != null;

        public bool CheckIfPasswordMatch(string username, string password)
        => Context.Users.Where(user => user.UserName == username && user.Password == password).FirstOrDefault() != null;

        public IEnumerable<User> GetAllUsers() => Context.Users;

        public User GetUser(string username, string password)
        => Context.Users.FirstOrDefault(user => user.UserName == username && user.Password == password);

        public string Get_User_Details(string username, string password)
        {
            User _user = Context.Users.FirstOrDefault(user => user.UserName == username);
            if (_user != null && _user.Password == password)
                return $"{_user.FirstName} {_user.LastName}";
            return null;
        }

        public void UpdateUser(User user)
        {
            User datauser = Context.Users.Where(u => u.Id == user.Id).FirstOrDefault();
            datauser.FirstName = user.FirstName;
            datauser.LastName = user.LastName;
            datauser.Password = user.Password;
            datauser.Email = user.Email;
            datauser.BirthDate = user.BirthDate;
            Context.SaveChanges();
        }
    }
}
