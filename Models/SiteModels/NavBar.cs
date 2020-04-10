using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dynamify.Models.SiteModels
{
    public class NavBar
    {
        [Key]
        public int nav_bar_id {get;set;}

        //saved and read as a series of links and labels e.g.
        //"label1{link1}label2{link2}label{link3}"
        //"{" signifies end of label and "}" signifies end of link
        //Entity workaround for lack of List<string>
        //functionality since order of links matters
        public string string_of_links {get;set;}
        public string type {get;set;} = "nav_bar";

        [Required]
        [ForeignKey("site")]
        public int site_id {get;set;}
        public Site site {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}