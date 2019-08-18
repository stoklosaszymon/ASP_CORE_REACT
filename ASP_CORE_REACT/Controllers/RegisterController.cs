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
    public class RegisterController : Controller
    {

        private readonly IHasher _hasher;
        private BloggingDBContext _context;

        public RegisterController(IHasher hasher, BloggingDBContext context)
        {
            _hasher = hasher;
            _context = context;
        }

        [HttpPost("[action]")]
        public RequestStatus AddUser([FromBody] Users user)
        {
            try
            {
                user.PasswordHash = _hasher.HashString(user.PasswordHash);
                _context.Users.Add(user);
                _context.SaveChanges();
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