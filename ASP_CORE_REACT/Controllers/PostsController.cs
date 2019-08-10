using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_CORE_REACT.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_CORE_REACT.Controllers
{
    
    [Route("api/[controller]")]
    public class PostsController : BaseController
    {
        [HttpPost("[action]")] 
        public void AddPost([FromBody] Posts post)
        {
            post.ReleaseDate = DateTime.Now;
            post.LastEditedDate = DateTime.Now;
            Database.Posts.Add(post);
            Database.SaveChanges();
        }

        [HttpGet("[action]/{postId}")]
        public Posts GetPost(int postId)
        {
            return Database.Posts.FirstOrDefault(e => e.PostId == postId);
        }

        [HttpGet("[action]")]
        public IEnumerable<PostsViewModel> GetAllPosts()
        {
            var result = new List<PostsViewModel>();
            var allPosts = Database.Posts.AsEnumerable();

            foreach (var item in allPosts)
            {
                var user = Database.Users.FirstOrDefault(usr => usr.UserId == item.UserId);
                result.Add(new PostsViewModel
                {
                    PostId = item.PostId,
                    Title = item.Title,
                    Content = item.Content,
                    ReleaseDate = item.ReleaseDate.ToString("dd/MM/yyyy"),
                    LastEditedDate = item.LastEditedDate,
                    UserName = user.UserName,
                    UserSurname = user.UserSurname,
                });
            }
            return result;
        }

        [HttpPost("[action]")]
        public void DeletePost([FromBody] Posts post)
        {
            Database.Remove(Database.Posts.FirstOrDefault(e => e.PostId == post.PostId));
            Database.SaveChanges();
        }

    }
}