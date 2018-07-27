using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoodFoodMKE.Models;
using Microsoft.AspNet.Identity;

namespace GoodFoodMKE.Controllers
{
    public class FarmsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Farms
        public ActionResult Index()
        {
            
          
            
            return View();
        }

        // GET: Farms/Details/5
        public ActionResult Details(int? id)
        {
            return View();
        }

        // GET: Farms/Create
        public ActionResult Create()
        {
            ViewBag.AddressId = new SelectList(db.Addresses, "Id", "AddressString");
            return View();
        }

        // POST: Farms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LocationId,AddressId")] Farm farm)
        {
            return View(farm);
        }

        // GET: Farms/Edit/5
        public ActionResult Edit(int? id)
        {
            return View();
        }

        // POST: Farms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LocationId,AddressId")] Farm farm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(farm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "Id", "AddressString", farm.AddressId);
            return View(farm);
        }

        // GET: Farms/Delete/5
        public ActionResult Delete(int? id)
        {
            return View();
        }

        // POST: Farms/Delete/5
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
