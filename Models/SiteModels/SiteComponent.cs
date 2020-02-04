using System; //for datetime
using System.ComponentModel.DataAnnotations;

namespace dynamify.Models.SiteModels
{
    public abstract class SiteComponent
    {
        [Required]
        public string title {get;set;}

        [Required]
        //determines order in which components are displayed
        public int priority {get;set;} = 0; 

        [Required]
        public abstract string type {get;set;} //used by frontend to determine how to build site component

        [Required]
        public int site_id {get;set;}

        public Site site {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}