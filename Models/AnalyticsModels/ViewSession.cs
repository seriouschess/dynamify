using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using dynamify.Models.SiteModels;

namespace dynamify.Models.AnalyticsModels
{
    public class ViewSession
    {
        [Key]
        public int session_id {get;set;}

        public string url {get;set;}

        public string token {get;set;}
        
        //time on homepage
        public int time_on_homepage {get;set;}

        public DateTime created_at {get;set;} = DateTime.Now;

        [ForeignKey("site")]
        public int site_id {get;set;}
        public Site site {get;set;}
    }
}