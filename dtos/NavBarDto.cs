using System.Collections.Generic;

namespace dynamify.dtos
{
    public class NavBarDto
    {
        public List<NavLinkDto> links {get;set;}

        public int site_id {get;set;}
    }
   
}