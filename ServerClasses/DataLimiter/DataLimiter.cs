using dynamify.Models;
using dynamify.Models.SiteModels;
using dynamify.ServerClasses.QueryClasses;

namespace dynamify.ServerClasses.DataLimiter
{
    public class DataLimiter
    {
        AdminQueries _dbQuery;
        public DataLimiter(AdminQueries dbQuery){
            _dbQuery = dbQuery;
        }

        //this method is kinda trash right now
        public int ConvertCharLengthToBytes(int character_sum){
            return character_sum;
        }

        //does not remove component itself.
        public void RemoveFromDataPlan(SiteComponent site_component, int admin_id){
            DataPlan admin_data_plan =_dbQuery.FindDataPlanByAdminId(admin_id);
            admin_data_plan.total_bytes -= site_component.FindCharLength();
            _dbQuery.UpdateDataPlan( admin_data_plan );
        }

        //adds data to plan if not above max
        public bool ValidateDataPlan(SiteComponent site_component, DataPlan data_plan){
            data_plan.total_bytes -= site_component.byte_size;
            site_component.byte_size = site_component.FindCharLength();
            data_plan.total_bytes += site_component.byte_size;
            if( data_plan.total_bytes >= data_plan.max_bytes ){
                return false;
            }else{
                return true;
            }
        }
    }
}