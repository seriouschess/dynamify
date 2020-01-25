using System; //datetime
using System.ComponentModel.DataAnnotations;
using dynamify.Models;

namespace dynamify.Models.SiteModels
{
    public class ParagraphBox
    {
        [Key]
        public int paragraph_box_id {get;set;}
        [Required]
        public string title {get;set;}
        [Required]
        public string content {get;set;}

        [Required]
        public int site_id {get;set;}
        public Site site {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}