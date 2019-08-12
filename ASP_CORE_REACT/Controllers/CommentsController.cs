using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public IEnumerable<CommentViewModel> GetCommentsForPost(int postId)
        {
            return Database.Comments.Where(com => com.PostId == postId)
            .Join(Database.Users,
                    com => com.UserId,
                    user => user.UserId,
                    (com, user) => new CommentViewModel
                    {
                        CommentId = com.CommentId,
                        CommentContent = com.CommentContent,
                        ReleaseDate = com.ReleaseDate.ToString("MM/dd/yyyy"),
                        UserName = user.UserName,
                        UserSurname = user.UserSurname,
                    })
                    .ToList();
        }

        [HttpPost("[action]")]
        public void AddCommentToPost([FromBody] Comments comment)
        {
            comment.ReleaseDate = DateTime.Now;
            comment.Edited = false;
            Database.Comments.Add(comment);
            Database.SaveChanges();
        }
    }
}