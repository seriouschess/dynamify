using dynamify.ServerClasses.DataLimiter;

namespace dynamify.Models.DataPlans
{
    public class FreeDataPlan:DataPlan
    {
        public FreeDataPlan(){
            this.max_bytes = (int)DataPlanTiers.Free; //10 megs
            this.premium_tier = "Free";
        }
    }
}