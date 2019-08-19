using ASP_CORE_REACT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_CORE_REACT.classes
{
    public interface IUsersService
    {
        Users AddUser(Users user);
        void RemoveUser(Users user);
        IEnumerable<Users> GetUsers();
        UserData GetUserNameById(int userId);
    }
}
