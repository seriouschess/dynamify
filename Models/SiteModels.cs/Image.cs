using System;
using System.ComponentModel.DataAnnotations;

namespace dynamify.Models.SiteModels.cs
{
    public class Image
    {
        [Key]
        int image_id {get;set;}

        [Required]
        string image_src {get;set;}

        [Required]
        int site_id {get;set;}

        Site site {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}