using System.ComponentModel.DataAnnotations;

namespace dynamify.Models.SiteModels
{
    public partial class Image : SiteComponent
    {
        [Key]
        public int image_id {get;set;}
        public override string type {get;set;} = "image";

        [Required]
        public string image_src {get;set;}
    }
}