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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public IEnumerable<Users> GetUsers()
        {
            return Database.Users.Select(n => n);
        }

        [HttpPost("[action]")]
        public IActionResult AddUser([FromBody] Users user)
        {
            if ( user == null)
            {
                return NoContent();
            }
            Database.Users.Add(user);
            Database.SaveChanges();

            return Redirect("/users");
        }

        [HttpPost("[action]")]
        public IActionResult RemoveUser([FromBody] Users user)
        {
            Database.Users.Remove(Database.Users.FirstOrDefault(e => e.UserId == user.UserId));
            Database.SaveChanges();

            return Redirect("/users");
        }
    }

}