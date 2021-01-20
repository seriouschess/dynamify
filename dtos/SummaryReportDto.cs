namespace dynamify.dtos
{
    public class SummaryReportDto
    {
        public int total_admins {get;set;}
        public int total_new_admins_this_month {get;set;} 
        public int total_sites {get;set;}
        public int total_new_sites_this_month {get;set;}
        public double total_storage_megabytes {get;set;}
    }
}