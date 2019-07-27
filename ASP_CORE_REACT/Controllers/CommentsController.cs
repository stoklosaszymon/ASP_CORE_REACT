using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_CORE_REACT.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_CORE_REACT.Controllers
{
    public class CommentsController : BaseController
    {
        [HttpPost]
        public IEnumerable<Comments> GetCommentsForPost([FromBody] Posts post)
        {
            return Database.Comments.Where(el => el.PostId == post.PostId);
        }

        [HttpPost]
        public void AddCommentToPost([FromBody] Comments comment)
        {
            Database.Comments.Add(comment);
            Database.SaveChanges();
        }
    }
}