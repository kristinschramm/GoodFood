﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoodFoodMKE.Models;
using GoodFoodMKE.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace GoodFoodMKE.Controllers
{
    public class FarmController : Controller
    {
        private ApplicationDbContext _context;
        public FarmController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Farm
        public ActionResult Index()
        {
            var viewModels = new List<FarmViewModel>();

            var farms = _context.Farms.OrderBy(f => f.Name).ToList();

            foreach (var farm in farms)
            {
                var viewModel = new FarmViewModel
                {
                    Farm = farm,
                    Markets = _context.Markets.ToList()
                };
                viewModels.Add(viewModel);
            }

            return View(viewModels);
        }

        //CREATE: Farm
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new CreateFarmViewModel()
            {
                AppUsers = _context.AppUsers.ToList(),
                RequestorId = User.Identity.GetUserId(),
                Products = _context.Products.ToList()

            };

            return View(viewModel);
                }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateFarmViewModel model)
        {
            var userId = User.Identity.GetUserId();
            var appUser = _context.AppUsers.Single(a => a.Id == userId);
            if (!ModelState.IsValid)
            {
                model.AppUsers = _context.AppUsers.ToList();
                model.RequestorId = User.Identity.GetUserId();
                model.Products = _context.Products.ToList();

                return View(model);
            }
            var newAddress = new Address()
            {
                AddressString = model.Farm.Address.AddressString,
            };
            _context.Addresses.Add(newAddress);

            var newFarm = new Farm()
            {
                AccountManagers = _context.AppUsers.Where(m => m.Id == appUser.Id).ToList(),
                Active = false,
                Address = newAddress,
                Name = model.Farm.Name,
                WebAddress = model.Farm.WebAddress
            };
            _context.Farms.Add(newFarm);
            _context.SaveChanges();

            return RedirectToAction("Index", "Farm");
        }
        [HttpPost]
        public ActionResult UploadFiles()
        {
            bool isSuccess = false;
            string serverMessage = string.Empty;
            var fileOne = Request.Files[0] as HttpPostedFileBase;            
            string uploadPath = ConfigurationManager.AppSettings["UPLOAD_PATH"].ToString();
            string newFileOne = Path.Combine(uploadPath, fileOne.FileName);
            
            fileOne.SaveAs(newFileOne);
            

            if (System.IO.File.Exists(newFileOne))
            {
                isSuccess = true;
                serverMessage = "File has been uploaded successfully";
            }
            else
            {
                isSuccess = false;
                serverMessage = "File upload has failed. Please try again.";
            }
            return Json(new { IsSucccess = isSuccess, ServerMessage = serverMessage }, JsonRequestBehavior.AllowGet);
        }
    }
}