using Blog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.ViewModels
{
    public class ListViewModel
    {
        public ListViewModel(IBlogRepository _blogRepository, int p)
        {
            Posts = _blogRepository.Posts(p, 10);
            TotalPosts = _blogRepository.TotalPosts();
            Categories = _blogRepository.Categories();
            LastPosts = _blogRepository.LastPosts();
            
        }

        public ListViewModel(IBlogRepository _blogRepository, string categorySlug, int p)
        {
            Posts = _blogRepository.PostsForCategory(categorySlug, p - 1, 10);
            TotalPosts = _blogRepository.TotalPostsForCategory(categorySlug);
            Category = _blogRepository.Category(categorySlug);
            Categories = _blogRepository.Categories();
            LastPosts = _blogRepository.LastPosts();
        }

        public ListViewModel(IBlogRepository _blogRepository, int p, string urlSlug)
        {
            Posts = _blogRepository.Post(urlSlug);
            TotalPosts = _blogRepository.TotalPosts();
            Categories = _blogRepository.Categories();
            LastPosts = _blogRepository.LastPosts();
        }

        public IList<Post> Posts { get; set; }
        public int TotalPosts { get; set; }
        public Category Category { get; set; }
        public IList<Category> Categories { get; set; }        
        public IList<Post> LastPosts { get; set; }
    }
}
