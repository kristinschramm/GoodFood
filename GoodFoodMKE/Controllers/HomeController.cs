using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoodFoodMKE.Models;
using GoodFoodMKE.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace GoodFoodMKE.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController ()
        {
            _context = new ApplicationDbContext();
        }
    public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult AdminHome()
        {
            var viewModel = new AdminViewModel
            {
                Farms = _context.Farms.Where(f => f.Active == false).ToList(),
                Markets = _context.Markets
                .Include(m => m.Requestor)
                    .Where(m => m.Active == false)
                    .ToList(),
                BlogEntries = _context.BlogEntries.Where(b => b.Approved == false).ToList()
            };
            return View(viewModel);
        }
    }
}