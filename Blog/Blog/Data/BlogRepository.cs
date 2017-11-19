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

            var posts = _context.Posts.Where(p => p.Published).Include(p => p.Category).Include(p => p.TagsPosts).ToList();

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

        public IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize)
        {
            var Posts = _context.Posts.Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                                .Skip(pageNo * pageSize)
                                .Take(pageSize)
                                .Include(p => p.Category)
                                .Include(p => p.TagsPosts)
                                .OrderByDescending(p => p.PostedOn)
                                .ToList();
            return Posts;
        }

        public int TotalPostsForCategory(string categorySlug)
        {
            var totalCategory = _context.Posts.Where(p => p.Category.UrlSlug.Equals(categorySlug)).Count();

            return totalCategory;
        }

        public Category Category(string categorySlug)
        {
            var Category = _context.Categories.Where(p => p.UrlSlug.Equals(categorySlug)).FirstOrDefault();

            return Category;
        }

        public IList<Category> Categories()
        {
            var Categories = _context.Categories.OrderBy(p => p.Name).ToList();

            return Categories;
        }

        public IList<Post> Post(string urlSlug)
        {
            var Post = _context.Posts.Where(p => p.UrlSlug.Equals(urlSlug)).ToList();

            return Post;
        }

        public IList<Post> LastPosts()
        {
            var LastPosts = _context.Posts.Where(p => p.Published).OrderByDescending(p => p.PostedOn).ToList();

            return LastPosts;
        }
    }
}
