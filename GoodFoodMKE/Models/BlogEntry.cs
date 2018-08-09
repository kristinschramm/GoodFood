using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoodFoodMKE.Models
{
    public class BlogEntry
    {
        [Key]
        public int BlogId { get; set; }
        public string Title { get; set; }
        public AppUser Creator { get; set; }
        public List<Comment> Comments { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Approved { get; set; }

    }
}