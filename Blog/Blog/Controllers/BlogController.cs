﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Data;
using Blog.Models;
using Blog.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public ViewResult Post(string urlslug,int p = 1)
        {
            var ViewModel = new ListViewModel(new BlogRepository(_blogContext), p, urlslug);

            ViewBag.Title = "Post";

            return View("Posts",ViewModel);
        }

        public ViewResult Create()
        {
            var Post = new Post
            {
                PostedOn = DateTime.Now,
                Published = true,
                Modified = DateTime.Now,                
            };

            PopulateCategoriesDropDownList();

            return View(Post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title, ShortDescription, Meta, UrlSlug, Published, PostedOn, Modified, CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {                
                _blogContext.Add(post);
                await _blogContext.SaveChangesAsync();
                return RedirectToAction(nameof(Posts));
            }
            PopulateCategoriesDropDownList("CategoryId");
            return View(post);
        }

        private void PopulateCategoriesDropDownList(object selectedCategory = null)
        {
            var categoryQuery = from category in _blogContext.Categories
                                   orderby category.Name
                                   select category;
            ViewBag.CategoryId = new SelectList(categoryQuery.AsNoTracking(), "CategoryId", "Name", selectedCategory);
        }

        public ActionResult Edit(int id)
        {
            var post = _blogContext.Posts.Where(p => p.PostId.Equals(id)).FirstOrDefault();

            PopulateCategoriesDropDownList(post.CategoryId);

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("PostId, Title, ShortDescription, Meta, UrlSlug, Published, PostedOn, Modified, CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {
                _blogContext.Update(post);
                await _blogContext.SaveChangesAsync();
                return RedirectToAction(nameof(Posts));
            }
            PopulateCategoriesDropDownList("CategoryId");
            return View(post);
        }
        
        public ActionResult Delete(int id)
        {
            var post = _blogContext.Posts.Where(p => p.PostId.Equals(id)).FirstOrDefault();

            if (!string.IsNullOrEmpty(post.PostId.ToString()))
            {
                _blogContext.Remove(post);
                _blogContext.SaveChanges();
                return RedirectToAction("Posts");
            }

            return View();
        }
    }
}