using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASP_CORE_REACT.Models;
using ASP_CORE_REACT.interfaces;
using ASP_CORE_REACT.classes;

namespace ASP_CORE_REACT.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpGet("[action]")]
        public IEnumerable<Users> GetUsers()
        {
            return _userService.GetUsers();
        }

        [HttpGet("[action]/{userId}")]
        public UserData GetUserNameById(int userId)
        {
            return _userService.GetUserNameById(userId);
        }

        [HttpPost("[action]")]
        public Users AddUser([FromBody] Users user)
        {
            _userService.AddUser(user);
            return user;
        }

        [HttpPost("[action]")]
        public void RemoveUser([FromBody] Users user)
        {
            _userService.RemoveUser(user);
        }
    }

}

public class UserData
{
    public string UserName { get; set; }
    public string UserSurname { get; set; }
}