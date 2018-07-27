using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoodFoodMKE.Models;

namespace GoodFoodMKE.Controllers
{
    public class MarketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Markets
        public ActionResult Index()
        {
                        return View();
        }

        // GET: Markets/Details/5
        public ActionResult Details(int? id)
        {
          
            return View();
        }

        // GET: Markets/Create
        public ActionResult Create()
        {
            ViewBag.AddressId = new SelectList(db.Addresses, "Id", "AddressString");
            return View();
        }

        // POST: Markets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LocationId,AddressId")] Market market)
        {
            return View();
        }

        // GET: Markets/Edit/5
        public ActionResult Edit(int? id)
        {
            return View();
        }

        // POST: Markets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LocationId,AddressId")] Market market)
        {
            if (ModelState.IsValid)
            {
                db.Entry(market).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "Id", "AddressString", market.AddressId);
            return View(market);
        }

        // GET: Markets/Delete/5
        public ActionResult Delete(int? id)
        {
            return View();
        }

        // POST: Markets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
