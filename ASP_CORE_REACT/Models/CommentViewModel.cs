using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_CORE_REACT.Models
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public string CommentContent { get; set; }

        public string UserName { get; set; }

        public string UserSurname { get; set; }

        public string ReleaseDate { get; set; }
        public bool? Edited { get; set; }
    }
}
