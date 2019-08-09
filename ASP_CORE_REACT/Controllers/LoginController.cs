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
    public class LoginController : BaseController
    {
        private readonly IHasher _hasher;
        
        public LoginController( IHasher hasher)
        {
            _hasher = hasher;
        }

        [HttpPost("[action]")]
        public bool SignIn([FromBody] Pass pass)
        {
            var foundUser = Database.Users.FirstOrDefault(user => user.Email == pass.Email);

            return foundUser != null ? foundUser.PasswordHash == _hasher.HashString(pass.Password) : false;
        }
    }


}