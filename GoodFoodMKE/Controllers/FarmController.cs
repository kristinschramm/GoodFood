using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoodFoodMKE.Models;
using GoodFoodMKE.Models.ViewModels;

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
    }
}