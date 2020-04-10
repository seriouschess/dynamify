using System.Collections.Generic;

namespace dynamify.dtos
{
     public class NavLinkDto
    {
        public string url {get;set;}
        public string label {get;set;}
    }

    public class NavBarDto
    {
        public List<NavLinkDto> links {get;set;}

        public int site_id {get;set;}
    }
   
}