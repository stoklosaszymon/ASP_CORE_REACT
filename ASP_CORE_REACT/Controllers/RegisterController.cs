using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASP_CORE_REACT.Models;
using ASP_CORE_REACT.interfaces;

namespace ASP_CORE_REACT.Controllers
{
    [Route("[controller]")]
    public class RegisterController : BaseController
    {

        private readonly IHasher _hasher;

        public RegisterController(IHasher hasher)
        {
            _hasher = hasher;
        }

        [HttpPost("[action]")]
        public bool AddUser([FromBody] Users user)
        {
            user.PasswordHash = _hasher.HashString(user.PasswordHash);
            Database.Users.Add(user);
            Database.SaveChanges();
            return true;
        }
    }
}