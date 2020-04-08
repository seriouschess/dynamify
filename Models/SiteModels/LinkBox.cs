using System.ComponentModel.DataAnnotations;

namespace dynamify.Models.SiteModels
{
    public class LinkBox : SiteComponent
    {
        [Key]
        public int link_box_id {get;set;}
        public override string type {get;set;} = "link_box";

        [Required]
        public string content {get;set;}

        [Required]
        public string url{get;set;}

        [Required]
        //what the user sees before they click
        public string link_display{get;set;}
    }
}