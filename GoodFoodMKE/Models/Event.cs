using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GoodFoodMKE.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string Title { get; set; }
        public int AddressId { get; set; }
        public Address EventAddress { get; set; }
        public int CreatorId { get; set; }
        public AppUser Creator { get; set; }
        public Farm Host { get; set; }
        public Market Market { get; set; }
        public List<DateTime> DateTimes { get; set; }
        public bool Recurring { get; set; }
        public string RecurringType { get; set; }
    }
}