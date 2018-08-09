using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GoodFoodMKE.Models.ViewModels
{
    public class AdminViewModel
    {
        public List<Farm> Farms { get; set; }

        public List<Market> Markets { get; set; }
        [Display(Name = "Requestor")]
        public string RequestorId { get; set; }
        public AppUser Requestor { get; set; }
        public List<BlogEntry> BlogEntries { get; set; }
        
    }
}