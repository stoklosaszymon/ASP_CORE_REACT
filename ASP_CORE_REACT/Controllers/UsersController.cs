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

        private BloggingDBContext _context;
        public UsersController(BloggingDBContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public IEnumerable<Users> GetUsers()
        {
            return _context.Users.AsEnumerable();
        }

        [HttpGet("[action]/{userId}")]
        public UserData GetUserNameById(int userId)
        {
            var foundUser = _context.Users.FirstOrDefault(user => user.UserId == userId);
            return new UserData { UserName = foundUser.UserName, UserSurname = foundUser.UserSurname };
        }

        [HttpPost("[action]")]
        public void AddUser([FromBody] Users user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        [HttpPost("[action]")]
        public void RemoveUser([FromBody] Users user)
        {
            var removeUser = _context.Users.FirstOrDefault(e => e.UserId == user.UserId);
            _context.Comments.RemoveRange(_context.Comments.Where(com => com.UserId == removeUser.UserId));
            _context.Posts.RemoveRange(_context.Posts.Where(post => post.UserId == removeUser.UserId));
            _context.Users.Remove(removeUser);
            _context.SaveChanges();
        }
    }

}

public class UserData
{
    public string UserName { get; set; }
    public string UserSurname { get; set; }
}