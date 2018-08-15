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
    public class EventController : Controller
    {
        private ApplicationDbContext _context;
        public EventController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var events = _context.Events.Include(m => m.Creator).Include(m => m.EventAddress).OrderBy(m =>m.DateTime).ToList();
            return View(events);
        }
        public ActionResult Create()
        {
            var tempEvent = new Event();
            return View(tempEvent);
        }
        [HttpPost]
        public ActionResult Create(Event inputEvent)
        {
            var appUserId = User.Identity.GetUserId();
            var tempEvent = new Event()
            {
               Creator= _context.AppUsers.Where(m => m.Id ==appUserId).Single(),
               EventAddress = inputEvent.EventAddress,
               Title = inputEvent.Title

            };

            _context.Events.Add(tempEvent);

            _context.SaveChanges();

            return RedirectToAction("Details", tempEvent.EventId);
        }

        public ActionResult Details(int id)
        {
            var tempEvent = _context.Events.Include(m => m.Creator).Where(m => m.EventId == id).Single();
            return View(tempEvent);
        }
        public ActionResult Delete(int id)
        {
            var tempEvent = _context.Events.Include(m => m.Creator).Where(m => m.EventId == id).Single();
            _context.Events.Remove(tempEvent);
            _context.SaveChanges();
            return View(tempEvent);
        }
    }

}
