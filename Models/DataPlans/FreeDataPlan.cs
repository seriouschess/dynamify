using dynamify.ServerClasses.DataLimiter;

namespace dynamify.Models.DataPlans
{
    public class FreeDataPlan:DataPlan
    {
        public FreeDataPlan(){
            this.max_bytes = (int)DataPlanTiers.Free; //10 megs
            this.max_sites = 5;
            this.premium_tier = "Free";
        }
    }
}