using System.ComponentModel.DataAnnotations;

namespace dynamify.Models.SiteModels
{
    public class NavLink
    {
        [Key]
        public int link_id {get;set;}
        public int nav_bar_id {get;set;}
        public string url {get;set;}
        public string label {get;set;}
    }
}