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
    public class MarketController : Controller
    {
        private ApplicationDbContext _context;
        public MarketController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var markets = _context.Markets.Include(m => m.Address).OrderBy(m => m.Name).ToList();
            return View(markets);
        }


        public ActionResult Details(int id)
        {
            var viewModel = new MarketViewModel()
            {
                Market = _context.Markets.Include(m => m.Address).Include(m => m.Requestor).Single(m => m.Id == id),

            };
            var connections = _context.MarketFarmConnections.Where(m => m.MarketId == viewModel.Market.Id).Where( m => m.Active == false).ToList();
            var farms = new List<Farm>();
            ; foreach (var c in connections)
            {
                var tempFarm = _context.Farms.Include(m => m.Address).Where(m => m.Id == c.FarmId).Single();
                farms.Add(tempFarm);
            };
            viewModel.PendingFarms = farms;

            var connectionsActive = _context.MarketFarmConnections.Where(m => m.MarketId == viewModel.Market.Id).Where(m => m.Active == true).ToList();
            var farmsActive = new List<Farm>();
            foreach (var c in connections)
            {
                var tempFarm = _context.Farms.Include(m => m.Address).Where(m => m.Id == c.FarmId).Single();
                farmsActive.Add(tempFarm);
            };
            viewModel.ActiveFarms = farmsActive;

            return View(viewModel);
        }
        public ActionResult Edit(int id)
        {
            var farms = new List<Farm>();
            var marketFarms = new List<MarketFarmConnection>();
            marketFarms = _context.MarketFarmConnections.Where(m => m.MarketId == id).ToList();
            var viewModel = new MarketViewModel()
            {
                Market = _context.Markets.Include(m => m.Address).Single(m => m.Id == id),


            };
            foreach (var marketFarm in marketFarms)
            {
                var farm = _context.Farms.Include(m => m.Address).Where(f => f.Id == marketFarm.FarmId).Single();
                farms.Add(farm);
            }
            viewModel.ActiveFarms = farms;

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Edit(MarketViewModel viewModel)
        {
            var dbMarket = _context.Markets.Include(m => m.Address).Where(m => m.Id == viewModel.Market.Id).Single();
            dbMarket.Address.AddressString = viewModel.Market.Address.AddressString;
            dbMarket.Name = viewModel.Market.Name;
            dbMarket.PhoneNumber = viewModel.Market.PhoneNumber;
            dbMarket.WebAddress = viewModel.Market.WebAddress;

            _context.SaveChanges();

            return RedirectToAction("Details", dbMarket.Id);

        }


        [HttpPost]
        public ActionResult UploadFiles(int id)
        {
            bool isSuccess = false;
            string serverMessage = string.Empty;
            var fileOne = Request.Files[0] as HttpPostedFileBase;
            string uploadPath = ConfigurationManager.AppSettings["UPLOAD_PATH"].ToString();
            string newFileOne = Path.Combine(uploadPath, fileOne.FileName);

            var market = _context.Markets.Where(m => m.Id == id).Single();
            market.LogoFilePath = "~/Image/" + fileOne.FileName;
            _context.SaveChanges();

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
            return Json(new { IsSucccess = isSuccess, ServerMessage = serverMessage, JsonRequestBehavior.AllowGet });
        }
        public new ActionResult Profile(string id)
        {
            var viewModel = new MarketViewModel
            {
                Market = _context.Markets.Include(m => m.Address).Include(m => m.Requestor).Where(m => m.RequestorId == id).Single()
            };

            var connections = _context.MarketFarmConnections.Where(m => m.MarketId == viewModel.Market.Id).Where(m => m.Active == false).ToList();
            var farms = new List<Farm>();
             foreach (var c in connections)
            {
                var tempFarm = _context.Farms.Include(m => m.Address).Where(m => m.Id == c.FarmId).Single();
                farms.Add(tempFarm);
            };
            viewModel.PendingFarms = farms;

            var connectionsActive = _context.MarketFarmConnections.Where(m => m.MarketId == viewModel.Market.Id).Where(m => m.Active == true).ToList();
            var farmsActive = new List<Farm>();
            foreach (var c in connections)
            {
                var tempFarm = _context.Farms.Include(m => m.Address).Where(m => m.Id == c.FarmId).Single();
                farmsActive.Add(tempFarm);
            };
            viewModel.ActiveFarms = farmsActive;

            return View("Details", viewModel);
        }

        public ActionResult Join(int id)
        {
            var appUserId = User.Identity.GetUserId();
            var tempMarket = _context.Markets.Include(m => m.Address).Where(m => m.Id == id).Single();
            var tempFarm = _context.Farms.Include(m => m.Address).Where(m => m.RequestorId == appUserId).Single();
            var connection = new MarketFarmConnection()
            {
                Active = false,
                MarketId = tempMarket.Id,
                FarmId = tempFarm.Id

            };

            _context.MarketFarmConnections.Add(connection);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ApproveFarm(int id)
        {


            var tempConnection = _context.MarketFarmConnections.Where(f => f.FarmId == id).Single();
            tempConnection.Active = true;
            _context.SaveChanges();


            return RedirectToAction("Profile");

        }

    }
}