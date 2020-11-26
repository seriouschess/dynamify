using System.Collections.Generic;

namespace dynamify.dtos
{
    public class SkeletonSiteDto
    {
        public int site_id {get;set;}

        public string title {get;set;}

        public List<SiteComponentDto> site_components {get;set;}
    }
}