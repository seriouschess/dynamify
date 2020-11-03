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

        public int ValidateDataPlan(SiteComponent site_component, DataPlan data_plan){
            data_plan.total_bytes -= site_component.byte_size;
            site_component.byte_size = site_component.FindCharLength();
            data_plan.total_bytes += site_component.byte_size;
            if( data_plan.total_bytes >= data_plan.max_bytes ){
                throw new System.ArgumentException($"Component data exceeds maximum for this data plan for Admin Id: {data_plan.admin_id}");
            }
            return data_plan.total_bytes;
        }
    }
}