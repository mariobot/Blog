using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Data;
using Blog.Models.ViewModels;
using System.Web;
using System.Net;
using Microsoft.AspNetCore.Http;

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


        public IActionResult Category(string category, int p = 1)
        {
            var ViewModel = new ListViewModel(new BlogRepository(_blogContext), category, p);

            if (ViewModel.Category == null)
                new BadRequestResult();

            ViewBag.Title = String.Format("Latest Posts in category {0}", ViewModel.Category.Name);

            return View("Posts", ViewModel);
        }
                
        public PartialViewResult Sidebars()
        {
            var widgetViewModel = new WidgetViewModel(new BlogRepository(_blogContext));

            return PartialView("_Sidebars",widgetViewModel);
        }

        //public IViewComponentResult InvokeAsync()
        //{            
        //    var widgetViewModel = new WidgetViewModel(new BlogRepository(_blogContext));
        //    //IEnumerable<tableRowClass> mc = await context.tableRows.ToListAsync();
        //    return PartialView("_Sidebars", widgetViewModel);
        //}

    }
}