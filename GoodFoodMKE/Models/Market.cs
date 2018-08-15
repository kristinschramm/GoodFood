using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GoodFoodMKE.Models
{
    public class Market
    {
        public int Id { get; set; }
        [Display(Name = "Market Name")]
        public string Name { get; set; }
        [Display(Name = "Street Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        [Display(Name = "Website Url")]
        public string WebAddress { get; set; }
        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }
        public string RequestorId { get; set; }
        public AppUser Requestor { get; set; }
        public bool Active { get; set; }
        public string LogoFilePath { get; set; }
        public string DayOpen { get; set; }
        public string TimeOpen { get; set; }
        public string TimeClose { get; set; }
    }
}