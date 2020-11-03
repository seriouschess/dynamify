using System.ComponentModel.DataAnnotations;

namespace dynamify.Models.SiteModels
{
    public partial class ParagraphBox: SiteComponent
    {
        [Key]
        public int paragraph_box_id {get;set;}
        public override string type {get;set;} = "p_box";

        [Required]
        public string content {get;set;}
    }
}