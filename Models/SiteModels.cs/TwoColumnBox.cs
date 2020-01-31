using System.ComponentModel.DataAnnotations;
using System;

namespace dynamify.Models.SiteModels.cs
{
    public class TwoColumnBox
    {
        [Key]
        public int two_column_box_id {get;set;}

        [Required]
        public string title_one {get;set;}

        [Required]
        public string title_two {get;set;}

        [Required]
        public string content_one {get;set;}

        [Required]
        public string content_two {get;set;}

        [Required]
        public int site_id {get;set;}
        public Site site {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}