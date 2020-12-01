using dynamify.ServerClasses.DataLimiter;

namespace dynamify.Models.DataPlans
{
    public class JohnHammondDataPlan : DataPlan //Spare No Expense
    {
        JohnHammondDataPlan(){
            max_bytes = (int)DataPlanTiers.JohnHammond;
            premium_tier = "JohnHammond";
        }
    }
}