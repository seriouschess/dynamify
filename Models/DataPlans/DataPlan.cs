using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dynamify.Models.DataPlans
{
    public class DataPlan
    {
        [Key]
        public int data_plan_id {get;set;}
        public int total_bytes {get;set;}
        public int max_bytes {get;set;} 
        public string premium_tier {get;set;}

        [ForeignKey("admin")]
        public int admin_id {get;set;}
        public Admin admin {get;set;}
    }
}