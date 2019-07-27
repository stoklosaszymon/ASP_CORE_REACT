using System;
using System.Collections.Generic;

namespace ASP_CORE_REACT.Models
{
    public partial class Users
    {
        public Users()
        {
            Comments = new HashSet<Comments>();
            Posts = new HashSet<Posts>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Posts> Posts { get; set; }
    }
}
