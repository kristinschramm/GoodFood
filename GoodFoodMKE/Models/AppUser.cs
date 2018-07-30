using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GoodFoodMKE.Models
{
    public class AppUser
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string NameFirst { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string NameLast { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Email Address")]

        public string EmailAddress { get; set; }

        public string GravatarEmailHash { get; set; }

        public List<AppUser> AccountManager { get; set; }


    }
}