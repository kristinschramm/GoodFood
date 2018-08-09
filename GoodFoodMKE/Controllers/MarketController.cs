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
        // GET: Farm
        public ActionResult Index()
        {
            var markets = _context.Markets.Include(m=>m.Address).OrderBy(m => m.Name).ToList();
            return View(markets);
        }

        //CREATE: Market
        [Authorize]
        public ActionResult Create()
        {
            var newMarket = new Market();
            _context.Markets.Add(newMarket);
            _context.SaveChanges();


            var viewModel = new CreateMarketViewModel()
            {
                Market = newMarket,
                RequestorId = User.Identity.GetUserId()
                

            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateMarketViewModel model)
        {
            var userId = User.Identity.GetUserId();
            var appUser = _context.AppUsers.Single(a => a.Id == userId);
            if (!ModelState.IsValid)
            {

                var newMarket = new Market();
                _context.Markets.Add(newMarket);
                _context.SaveChanges();


                var viewModel = new CreateMarketViewModel()
                {
                    Market = newMarket,
                    RequestorId = User.Identity.GetUserId()


                };

                return View(model);
            }
            var newAddress = new Address()
            {
                AddressString = model.Market.Address.AddressString,
            };
            _context.Addresses.Add(newAddress);

            var tempMarket = _context.Markets.Include(m =>m.AccountManagers).Single(m => m.Id == model.Market.Id);
            tempMarket.AccountManagers = _context.AppUsers.Where(m => m.Id == appUser.Id).ToList();
            tempMarket.Active = false;
            tempMarket.Address = newAddress;
            tempMarket.Name = model.Market.Name;
            tempMarket.WebAddress = model.Market.WebAddress;
                        
            _context.SaveChanges();

            return RedirectToAction("Index", "Market");
        }
        public ActionResult Details(int id)
        {
            var market = _context.Markets.Include(m => m.FarmIds).Single(m => m.Id == id);

            return View(market);
        }

        [HttpPost]
        public ActionResult UploadFiles(Market market)
        {
            bool isSuccess = false;
            string serverMessage = string.Empty;
            var fileOne = Request.Files[0] as HttpPostedFileBase;
            string uploadPath = ConfigurationManager.AppSettings["UPLOAD_PATH"].ToString();
            string newFileOne = Path.Combine(uploadPath, fileOne.FileName);
            string logoFilePath = fileOne.FileName;

            var tempMarket = _context.Markets.Single(m => m.Id == market.Id);
            tempMarket.LogoFilePath = logoFilePath;

            _context.SaveChanges();
            //pass through market object to store filename 

            
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