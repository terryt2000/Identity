using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Identity.Models
{
    [Table("BlogPost")]
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }
        public string Body { get; set; }

        [ForeignKey("UserId")]
        ApplicationUser User { get; set; }
    }
}