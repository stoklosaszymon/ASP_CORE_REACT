using System;
using System.Collections.Generic;

namespace ASP_CORE_REACT.Models
{
    public partial class Comments
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int? UserId { get; set; }
        public string CommentContent { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool? Edited { get; set; }

        public virtual Posts Post { get; set; }
        public virtual Users User { get; set; }
    }
}
