using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dynamify.Models.SiteModels
{
    public class NavBar
    {
        [Key]
        public int nav_bar_id {get;set;}

        public string type {get;set;} = "nav_bar";

        [Required]
        [ForeignKey("site")]
        public int site_id {get;set;}
        public Site site {get;set;}

        public List<NavLink> links {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}