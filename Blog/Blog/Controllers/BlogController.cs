using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Data;
using Blog.Models.ViewModels;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        //private readonly IBlogRepository _blogRepository;

        private readonly BlogContext _blogContext;

        public BlogController(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public ViewResult Posts(int p = 1)
        {
            var ViewModel = new ListViewModel(new BlogRepository(_blogContext), p);

            ViewBag.Title = "Last Posts";

            return View(ViewModel);
        }
    }
}