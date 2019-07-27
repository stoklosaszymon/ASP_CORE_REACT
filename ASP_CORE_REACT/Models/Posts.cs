using System;
using System.Collections.Generic;

namespace ASP_CORE_REACT.Models
{
    public partial class Posts
    {
        public Posts()
        {
            Comments = new HashSet<Comments>();
        }

        public int PostId { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime? LastEditedDate { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
