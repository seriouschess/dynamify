using System;
using System.ComponentModel.DataAnnotations;

namespace dynamify.Models
{
    public class Admin
    {
        [Key]
        public int AdminId {get;set;}
        [Required]
        public string FirstName {get;set;}
        [Required]
        public string LastName {get;set;}
        [Required]
        public string Email {get;set;}
        [Required]
        public string Password {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}