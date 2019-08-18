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
    public class PostsController : Controller
    {
        private BloggingDBContext _context;
        public PostsController(BloggingDBContext context)
        {
            _context = context;
        }

        [HttpPost("[action]")] 
        public void AddPost([FromBody] Posts post)
        {
            post.ReleaseDate = DateTime.Now;
            post.LastEditedDate = DateTime.Now;
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        [HttpGet("[action]/{postId}")]
        public Posts GetPost(int postId)
        {
            return _context.Posts.FirstOrDefault(e => e.PostId == postId);
        }

        [HttpGet("[action]")]
        public IEnumerable<PostsViewModel> GetAllPosts()
        {
           return _context.Posts
               .Join(_context.Users,
                       post => post.UserId,
                       usr => usr.UserId,
                       (post, usr) => new PostsViewModel
                       {
                           PostId = post.PostId,
                           Title = post.Title,
                           Content = post.Content,
                           ReleaseDate = post.ReleaseDate.ToString("dd/MM/yyyy"),
                           LastEditedDate = post.LastEditedDate,
                           UserName = usr.UserName,
                           UserSurname = usr.UserSurname,
                       })
                       .ToList();
        }

        [HttpPost("[action]")]
        public void DeletePost([FromBody] Posts post)
        {
            _context.Remove(_context.Posts.FirstOrDefault(e => e.PostId == post.PostId));
            _context.SaveChanges();
        }

    }
}