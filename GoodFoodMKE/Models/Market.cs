using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodFoodMKE.Models
{
    public class Market:Location
    {
        public List<Farm> Farms { get; set; }
    }
}