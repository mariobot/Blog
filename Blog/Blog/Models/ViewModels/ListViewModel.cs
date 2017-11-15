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
        }

        public IList<Post> Posts { get; set; }
        public int TotalPosts { get; set; }
    }
}
