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
        public List<Farm> Farms { get; set; }
        public List<Market> Markets { get; set; }
        
        public DateTime DateTime { get; set; }

        [Display(Name = "Day of Week")]
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime Time { get; set; }
    }
}