using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace GoodFoodMKE.Models
{
    public class Farm 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
         public int AddressId { get; set; }
        public Address Address { get; set; }
        public List<Product> Products { get; set; }
        
    }
}