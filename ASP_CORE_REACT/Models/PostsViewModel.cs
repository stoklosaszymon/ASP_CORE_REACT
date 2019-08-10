using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_CORE_REACT.Models
{
    public class PostsViewModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ReleaseDate { get; set; }
        public DateTime? LastEditedDate { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
    }
}
