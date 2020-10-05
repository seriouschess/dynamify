using System.Collections.Generic;
using dynamify.Models;
using dynamify.Models.SiteModels;

namespace dynamify.dtos
{
    public class SkeletonSiteDto
    {
        public int site_id {get;set;}

        public string title {get;set;}

        public List<SiteComponentDto> site_components {get;set;}
    }
}