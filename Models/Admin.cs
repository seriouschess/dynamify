using System;
using System.ComponentModel.DataAnnotations;

namespace dynamify.Models
{
    public class Admin
    {
        [Key]
        public int adminId {get;set;}
        [Required]
        public string firstName {get;set;}
        [Required]
        public string lastName {get;set;}
        [Required]
        public string email {get;set;}
        [Required]
        public string password {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}