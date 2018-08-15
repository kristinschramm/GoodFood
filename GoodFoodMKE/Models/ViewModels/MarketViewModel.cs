using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GoodFoodMKE.Models.ViewModels
{
    public class MarketViewModel
    {
   
        public Market Market { get; set; }

        public List<Farm> PendingFarms { get; set; }

        public List<Farm>ActiveFarms { get; set; }

       
    }

    public class CreateMarketViewModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string NameFirst { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string NameLast { get; set; }
               
        public Market Market { get; set; }

    }

}
