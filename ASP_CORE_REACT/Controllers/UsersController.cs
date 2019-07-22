using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ASP_CORE_REACT.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public IEnumerable<User> GetUsers()
        {
            return Enumerable.Range(1, 5).Select(index => new User
            {
                Name = "Jan",
                Surname = "Nowak"
            });
        }
    }

    public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}