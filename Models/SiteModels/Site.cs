using System; //for datetime
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using dynamify.Models.AnalyticsModels;

namespace dynamify.Models.SiteModels
{
    public class Site
    {
        [Key]
        public int site_id {get;set;}

        [Required]
        public string title {get;set;}

        public string url {get;set;}

        public bool active {get;set;} = false; 

        [Required]
        [ForeignKey("owner")]
        public int admin_id {get;set;}
        public Admin owner {get;set;}

        //navigational properties
        [InverseProperty("site")]
        public List<NavBar> nav_bars {get;set;} //only one should exist per site

        [InverseProperty("site")]
        public List<ParagraphBox> paragraph_boxes {get;set;}

        [InverseProperty("site")]
        public List<Image> images {get;set;}

        [InverseProperty("site")]
        public List<TwoColumnBox> two_column_boxes {get;set;}

        [InverseProperty("site")]
        public List<Portrait> portraits {get;set;}

        [InverseProperty("site")]
        public List<LinkBox> link_boxes {get;set;}

        [InverseProperty("site")]
        public List<ViewSession> views {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    }
}