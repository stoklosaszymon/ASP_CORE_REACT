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
        public IEnumerable<CommentViewModel> GetCommentsForPost(int postId)
        {
            var foundComments = Database.Comments.Where(el => el.PostId == postId);

            var result = new List<CommentViewModel>();
            foreach (var item in foundComments)
            {
                var user = Database.Users.FirstOrDefault(usr => usr.UserId == item.UserId);
                result.Add(new CommentViewModel
                {
                    CommentId = item.CommentId,
                    CommentContent = item.CommentContent,
                    UserName = user.UserName,
                    UserSurname = user.UserSurname,
                    ReleaseDate = item.ReleaseDate.ToString("dd/MM/yyyy"),
                    Edited = item.Edited
                });
            }
            return result;
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