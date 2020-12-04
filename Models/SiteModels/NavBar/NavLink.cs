using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dynamify.Models.SiteModels
{
    public partial class NavLink
    {
        [Key]
        public int link_id {get;set;}

        [Required]
        [ForeignKey("nav_bar")]
        public int nav_bar_id {get;set;}
        public NavBar nav_bar {get;set;}
        
        public string url {get;set;}
        public string label {get;set;}
    }
}