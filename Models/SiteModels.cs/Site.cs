using System; //for datetime
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dynamify.Models.SiteModels
{
    public class Site
    {
        [Key]
        public int site_id {get;set;}

        [Required]
        public string title{get;set;}

        public bool active {get;set;} = false; 

        [Required]
        public int admin_id {get;set;}
        public Admin owner {get;set;}

        public List<ParagraphBox> paragraph_boxes {get;set;}

        //public list[Admin] colaberators {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    }
}