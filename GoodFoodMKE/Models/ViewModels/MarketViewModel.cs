using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GoodFoodMKE.Models.ViewModels
{
    public class MarketViewModel
    {
   
        public Market Market { get; set; }

        public List<Farm> Farms { get; set; }


    }

    public class CreateMarketViewModel
    {
        [Display(Name = "Requestor")]
        public string RequestorId { get; set; }
        
        public Market Market { get; set; }

    }

}
