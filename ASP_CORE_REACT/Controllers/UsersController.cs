using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASP_CORE_REACT.Models;

namespace ASP_CORE_REACT.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        [HttpGet("[action]")]
        public IEnumerable<Users> GetUsers()
        {
            return Database.Users.AsEnumerable();
        }

        [HttpGet("[action]/{userId}")]
        public UserData GetUserNameById(int userId)
        {
            var foundUser = Database.Users.FirstOrDefault(user => user.UserId == userId);
            return new UserData { UserName = foundUser.UserName, UserSurname = foundUser.UserSurname };
        }

        [HttpPost("[action]")]
        public void AddUser([FromBody] Users user)
        {
            Database.Users.Add(user);
            Database.SaveChanges();
        }

        [HttpPost("[action]")]
        public void RemoveUser([FromBody] Users user)
        {
            Database.Users.Remove(Database.Users.FirstOrDefault(e => e.UserId == user.UserId));
            Database.SaveChanges();
        }
    }

}

public class UserData
{
    public string UserName { get; set; }
    public string UserSurname { get; set; }
}