using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_CORE_REACT.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_CORE_REACT.Controllers
{
    [Route("api/[controller]")]
    public class CommentsController : BaseController
    {
        [HttpGet("[action]/{postId}")]
        public IEnumerable<Comments> GetCommentsForPost(int postId)
        {
            return Database.Comments.Where(el => el.PostId == postId);
        }

        [HttpPost]
        public void AddCommentToPost([FromBody] Comments comment)
        {
            comment.ReleaseDate = DateTime.Now;
            comment.Edited = false;
            Database.Comments.Add(comment);
            Database.SaveChanges();
        }
    }
}