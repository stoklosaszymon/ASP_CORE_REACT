using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ASP_CORE_REACT.Controllers
{
    public class LoginController : BaseController
    {
        [Route("[controller]")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public bool SignIn([FromBody] Passes pass)
        {
            var foundUser = Database.Users.FirstOrDefault(user => user.Email == pass.Email);
            return foundUser != null ? foundUser.PasswordHash == pass.PasswordHash : false;
        }
    }

    public class Passes
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}