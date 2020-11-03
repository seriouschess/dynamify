using System.ComponentModel.DataAnnotations;

namespace dynamify.Models.SiteModels
{
    public partial class Portrait : SiteComponent
    {
        [Key]
        public int portrait_id {get;set;}

        public override string type {get;set;} = "portrait";

        [Required]
        public string image_src {get;set;}

        [Required]
        public string content {get;set;}
    }
}