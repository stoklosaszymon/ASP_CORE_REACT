using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace ASP_CORE_REACT.Controllers
{
    [Route("[controller]")]
    public class LoginController : BaseController
    {
        public IActionResult Index()
        {
            return Content("asd");
        }

        [HttpPost("[action]")]
        public bool SignIn([FromBody] Pass pass)
        {
            var foundUser = Database.Users.FirstOrDefault(user => user.Email == pass.Email);

            return foundUser != null ? foundUser.PasswordHash == Hasher.HashString(pass.Password) : false;
        }
    }

    public static class Hasher
    {
        public static string HashString(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }

    public class Pass
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}