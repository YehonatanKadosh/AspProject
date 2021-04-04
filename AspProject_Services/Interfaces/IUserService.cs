using AspProject_Entities.Models;
using System.Collections.Generic;

namespace AspProject_Services.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<User> GetAllUsers();
        bool CheckIfExists(string username);
        void AddUser(User user);
        string Get_User_Details(string username, string password);
        bool GetUser(string username, string password, out User user);
        User GetUser(string username, string password);
        bool CheckIfPasswordMatch(string username, string password);
        void UpdateUser(User user);
    }
}
