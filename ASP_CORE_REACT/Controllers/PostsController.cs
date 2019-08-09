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

        [HttpPost("[action]")]
        public Posts GetPost([FromBody] Posts post)
        {
            return Database.Posts.FirstOrDefault(e => e.PostId == post.PostId);
        }

        [HttpGet("[action]")]
        public IEnumerable<Posts> GetAllPosts()
        {
            return Database.Posts.AsEnumerable();
        }

        [HttpPost("[action]")]
        public void DeletePost([FromBody] Posts post)
        {
            Database.Remove(Database.Posts.FirstOrDefault(e => e.PostId == post.PostId));
            Database.SaveChanges();
        }

    }
}