using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GoodFoodMKE.Models.ViewModels
{
    public class FarmViewModel
    {
        public Farm Farm { get; set; }

        public List<Market> Markets { get; set; }
        public List<Product> Products { get; set; }

       
    }

    public class CreateFarmViewModel
    {
        [Display(Name = "Requestor" )]
        public string RequestorId { get; set; }
        public List<AppUser> AppUsers { get; set; }
        public List<Product> Products { get; set; }
        public Farm Farm { get; set; }

    }
    
}