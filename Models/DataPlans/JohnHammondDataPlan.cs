using dynamify.ServerClasses.DataLimiter;

namespace dynamify.Models.DataPlans
{
    public class JohnHammondDataPlan : DataPlan //Spare No Expense
    {
        JohnHammondDataPlan(){
            this.max_bytes = (int)DataPlanTiers.JohnHammond;
            this.max_sites = 500;
            this.premium_tier = "JohnHammond";
        }
    }
}