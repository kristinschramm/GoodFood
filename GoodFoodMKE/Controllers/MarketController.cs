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
            var viewModels = new List<MarketViewModel>();

            var markets = _context.Markets.OrderBy(m => m.Name).ToList();

            foreach (var market in markets)
            {
                var viewModel = new MarketViewModel
                {
                    Market = market,
                    Farms = _context.Farms.ToList()
                };
                viewModels.Add(viewModel);
            }

            return View(viewModels);
        }

        //CREATE: Market
    [Authorize]
    public ActionResult Create()
    {
        var viewModel = new CreateMarketViewModel()
        {
                AppUsers = _context.AppUsers.ToList(),
                RequestorId = User.Identity.GetUserId(),
                Farms = _context.Farms.ToList()

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
                model.AppUsers = _context.AppUsers.ToList();
                model.RequestorId = User.Identity.GetUserId();
                model.Farms = _context.Farms.ToList();

                return View(model);
            }
            var newAddress = new Address()
            {
                AddressString = model.Market.Address.AddressString,
            };
            _context.Addresses.Add(newAddress);

            var newMarket = new Market()
            {
                AccountManagers = _context.AppUsers.Where(m => m.Id == appUser.Id).ToList(),
                Active = false,
                Address = newAddress,
                Name = model.Market.Name,
                WebAddress = model.Market.WebAddress
            };
            _context.Markets.Add(newMarket);
            _context.SaveChanges();

            return RedirectToAction("Index", "Market");
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

