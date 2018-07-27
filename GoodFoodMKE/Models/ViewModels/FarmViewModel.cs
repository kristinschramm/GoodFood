using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodFoodMKE.Models.ViewModels
{
    public class FarmViewModel
    {
        public Farm Farm { get; set; }

        public List<Market> Markets { get; set; }
    }
}