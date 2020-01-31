using System;
using System.ComponentModel.DataAnnotations;

namespace dynamify.Models.SiteModels.cs
{
    public class Portrait
    {
        [Key]
        int portrait_id {get;set;}

        [Required]
        string portrait_title {get;set;}

        [Required]
        string portrait_content {get;set;}

       [Required]
        public int site_id {get;set;}
        public Site site {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}