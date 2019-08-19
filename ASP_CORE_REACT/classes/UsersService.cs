using ASP_CORE_REACT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_CORE_REACT.classes
{
    public class UsersService : IUsersService
    {
        private readonly BloggingDBContext _context;

        public UsersService(BloggingDBContext context)
        {
            _context = context;
        }

        public Users AddUser(Users user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        public void RemoveUser(Users user)
        {
            var removeUser = _context.Users.FirstOrDefault(e => e.UserId == user.UserId);
            _context.Comments.RemoveRange(_context.Comments.Where(com => com.UserId == removeUser.UserId));
            _context.Posts.RemoveRange(_context.Posts.Where(post => post.UserId == removeUser.UserId));
            _context.Users.Remove(removeUser);
            _context.SaveChanges();
        }
        public IEnumerable<Users> GetUsers()
        {
            return _context.Users.AsEnumerable();
        }

        public UserData GetUserNameById(int userId)
        {
            var foundUser = _context.Users.FirstOrDefault(user => user.UserId == userId);
            return new UserData { UserName = foundUser.UserName, UserSurname = foundUser.UserSurname };
        }
    }
}
