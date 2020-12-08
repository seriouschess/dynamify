using dynamify.ServerClasses.DataLimiter;

namespace dynamify.Models.DataPlans
{
    public class PremiumDataPlan:DataPlan //because equality isn't porfitable
    {
        PremiumDataPlan(){
            this.max_bytes = (int)DataPlanTiers.Premium; //1 gig
            this.max_sites = 50;
            this.premium_tier = "Premium";
        }
    }
}