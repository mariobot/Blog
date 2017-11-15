using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Blog.Models;

namespace Blog.Data
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogContext _context;

        public BlogRepository(BlogContext context)
        {
            _context = context;
        }

        public IList<Post> Posts(int pageNo, int pageSize)
        {

            var posts = _context.Posts.Where(p => p.Published).ToList();

            var postIds = posts.Select(p => p.PostId).ToList();
 
            return _context.Posts
              .Where(p => postIds.Contains(p.PostId))
              .OrderByDescending(p => p.PostedOn)                            
              .ToList();
        }

        public int TotalPosts()
        {
            return _context.Posts.Where(p => p.Published).Count();
        }
    }
}
