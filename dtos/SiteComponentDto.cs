namespace dynamify.dtos
{
    public class SiteComponentDto
    {
        public int component_id {get;set;}

        //determines order in which components are displayed
        public int priority {get;set;} = 0; 

        public string type {get;set;} //used by frontend to determine how to build site component

    }
}