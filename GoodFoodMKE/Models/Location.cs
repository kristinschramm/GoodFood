using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GoodFoodMKE.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public LocationType LocationType { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; } 

    }
}