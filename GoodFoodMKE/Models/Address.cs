﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace GoodFoodMKE.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressString { get; set; }
    }
}