using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GoodFoodMKE.Models
{
    public class MarketFarmConnection
    {
        [Key]
        public int Id { get; set; }
        public int FarmId { get; set; }
        public int MarketId { get; set; }
        public bool Active { get; set; }
    }
}