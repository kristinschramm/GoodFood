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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

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
                Farms = _context.Farms.Include(f => f.Requestor).Where(f => f.Active == false).ToList(),
                Markets = _context.Markets.Include(m => m.Requestor)
                    .Where(m => m.Active == false)
                    .ToList(),
                BlogEntries = _context.BlogEntries.Include(b => b.Creator).Where(b => b.Approved == false).ToList()
            };
            return View(viewModel);
        }
        
        public ActionResult ApproveFarm(int id)
        {
            
            
                var tempFarm = _context.Farms.Where(f => f.Id == id).Single();
                tempFarm.Active = true;
                _context.SaveChanges();
            
            
            return RedirectToAction("AdminHome");
        }

        public ActionResult ApproveMarket(int id)
        {
            var tempMarket = _context.Markets.Where(m => m.Id == id).Single();
            tempMarket.Active = true;
            _context.SaveChanges();


            return RedirectToAction("AdminHome");
        }

        public ActionResult ApproveBlog(int id)
        {


            var tempBlog = _context.BlogEntries.Where(b => b.BlogId == id).Single();
            tempBlog.Approved = true;
            _context.SaveChanges();


            return RedirectToAction("AdminHome");
        }
    }
}