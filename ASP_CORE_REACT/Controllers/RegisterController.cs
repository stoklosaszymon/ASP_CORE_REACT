using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASP_CORE_REACT.Models;
using ASP_CORE_REACT.interfaces;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ASP_CORE_REACT.classes;

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
        public RequestStatus AddUser([FromBody] Users user)
        {
            try
            {
                user.PasswordHash = _hasher.HashString(user.PasswordHash);
                Database.Users.Add(user);
                Database.SaveChanges();
            } catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("UNIQUE"))
                {
                    return new RequestStatus { Status = -1, Message = "specyfic email already egsists" };
                }
            }
            return new RequestStatus { Status = 1, Message = "account created" };
        }
    }
}