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

            var farms = _context.Farms.Include(f => f.Address).OrderBy(f => f.Name).ToList();

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
        public new ActionResult Profile(string id)
        {
            var viewModel = new FarmViewModel
            {
                Farm = _context.Farms.Include(f => f.Address).Include(f => f.Requestor).Where(f => f.RequestorId == id).Single()

            };
            var tempMarketConnections = _context.MarketFarmConnections.Where(mf => mf.FarmId == viewModel.Farm.Id).ToList();
            var markets = new List<Market>(); 
            foreach(var market in tempMarketConnections)
            {
                var tempMarket = _context.Markets.Include(m => m.Address).Include(m => m.Requestor).Where(m => m.Id == market.MarketId).Single();
                markets.Add(tempMarket);
            }
            viewModel.Markets = markets;

            return View("Details", viewModel);
        }
    }
}