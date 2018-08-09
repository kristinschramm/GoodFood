﻿using System;
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
        public List<string> FarmIds { get; set; }
        public List<AppUser> AccountManagers { get; set; }
        public string RequestorId { get; set; }
        public AppUser Requestor { get; set; }
        public bool Active { get; set; }
        public string LogoFilePath { get; set; }
    }
}