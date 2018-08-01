using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoodFoodMKE.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public AppUser Commentor { get; set; }
        public string CommentString { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpVote { get; set; }
        public int DownVote { get; set; }
        public Comment HeadComment { get; set; }
        public int BlogId { get; set; }
        public BlogEntry HeadEntry { get; set; }

    }
}