using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASP_CORE_REACT.Models;

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
        public IEnumerable<Users> GetUsers()
        {
            BloggingDBContext db = new BloggingDBContext();
            return db.Users.Select(n => n);
        }
    }
}