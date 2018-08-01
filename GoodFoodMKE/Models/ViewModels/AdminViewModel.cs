using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodFoodMKE.Models.ViewModels
{
    public class AdminViewModel
    {
        public List<Farm> Farms { get; set; }

        public List<Market> Markets { get; set; }
        
    }
}