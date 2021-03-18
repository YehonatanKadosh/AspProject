using AspProject_Entities.Models;
using System.Collections.Generic;

namespace AspProject_Services.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<User> GetAllUsers();
    }
}
