using dynamify.Models;
using dynamify.Models.SiteModels;

namespace dynamify.ServerClasses.DataLimiter
{
    public class DataLimiter
    {
        public DataLimiter(){

        }

        //this method makes estimates
        public int ConvertCharLengthToBytes(int character_sum){
            return character_sum;
        }

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