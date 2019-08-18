using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using ASP_CORE_REACT.interfaces;
using ASP_CORE_REACT.classes;
using ASP_CORE_REACT.Models;

namespace ASP_CORE_REACT.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IHasher _hasher;
        private BloggingDBContext _context;

        public LoginController( IHasher hasher, BloggingDBContext context)
        {
            _hasher = hasher;
            _context = context;
        }

        [HttpPost("[action]")]
        public int SignIn([FromBody] Pass pass)
        {
            var foundUser = _context.Users.FirstOrDefault(user => user.Email == pass.Email);
            var checkPassword = foundUser.PasswordHash == _hasher.HashString(pass.Password);
            return foundUser != null && checkPassword ? foundUser.UserId : 0;
        }
    }


}