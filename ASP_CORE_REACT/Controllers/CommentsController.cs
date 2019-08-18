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
    public class CommentsController : Controller
    {

        private BloggingDBContext _context;
        public CommentsController(BloggingDBContext context)
        {
            _context = context;
        }
        [HttpGet("[action]/{postId}")]
        public IEnumerable<CommentViewModel> GetCommentsForPost(int postId)
        {
            return _context.Comments.Where(com => com.PostId == postId)
            .Join(_context.Users,
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
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}