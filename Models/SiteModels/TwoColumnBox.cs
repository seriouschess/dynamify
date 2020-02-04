using System.ComponentModel.DataAnnotations;

namespace dynamify.Models.SiteModels
{
    public class TwoColumnBox : SiteComponent
    {
        [Key]
        public int two_column_box_id {get;set;}

        public override string type {get;set;} = "2c_box";

        [Required]
        public string heading_one {get;set;}

        [Required]
        public string heading_two {get;set;}

        [Required]
        public string content_one {get;set;}

        [Required]
        public string content_two {get;set;}
    }
}