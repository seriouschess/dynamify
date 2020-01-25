using System; //for datetime
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using dynamify.Models.SiteModels;

namespace dynamify.Models
{
    public class Admin
    {
        [Key]
        public int admin_id {get;set;}
        [Required]
        public string first_name {get;set;}
        [Required]
        public string last_name {get;set;}
        [Required]
        public string email {get;set;}
        [Required]
        public string password {get;set;}
        
        public List<Site> sites_owned {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}