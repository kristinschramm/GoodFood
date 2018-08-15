using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using GoodFoodMKE.Models;
using GoodFoodMKE.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Data.Entity;

namespace GoodFoodMKE.Controllers
{
    public class BlogController : Controller
    {
        private ApplicationDbContext _context;
        public BlogController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var blogs = _context.BlogEntries.Include(m => m.Creator).Include(m => m.Comments).OrderBy(m => m.CreatedDate).ToList();
            return View(blogs);
        }

        public ActionResult Create()
        {
            var blog = new BlogEntry();
            return View(blog);
        }
        [HttpPost]
        public ActionResult Create(BlogEntry blog)
        {
            var appUserId = User.Identity.GetUserId();
            var tempBlog = new BlogEntry()
            {
                Approved = false,
                Content = blog.Content,
                CreatedDate = DateTime.Now,
                Creator = _context.AppUsers.Where(a => a.Id == appUserId).Single(),
                Title = blog.Title,
                
            };

            _context.BlogEntries.Add(tempBlog);

            _context.SaveChanges();

            return RedirectToAction("Details", tempBlog.BlogId);
        }
        
        public ActionResult Details (int id)
        {
            var tempBlog = _context.BlogEntries.Include(m => m.Creator).Where(m => m.BlogId == id).Single();
            return View(tempBlog);
        }
    }
}