using dynamify.Models;
using dynamify.Models.SiteModels;
using dynamify.ServerClasses.QueryClasses;

namespace dynamify.ServerClasses.DataLimiter
{
    public class DataLimiter
    {
        AdminQueries _dbQuery;
        private int _site_container_size = 0; //may be increased
        private int _free_tier_site_limit = 5;
        public DataLimiter(AdminQueries dbQuery){
            _dbQuery = dbQuery;
        }

        //site
        public DataPlan ValidateSiteAdditionForDataPlan(int admin_id){

            int total_site_count = _dbQuery.GetSiteCountForAdminId(admin_id);
            if(!(total_site_count < _free_tier_site_limit)){ //must be strictly less than. Will add a site.
                throw new System.ArgumentException($"New site adddition exceeds maximum site count of {total_site_count}.");
            }
            
            DataPlan data_plan =_dbQuery.GetDataPlanByAdminId(admin_id);
            data_plan.total_bytes += this._site_container_size;

            if( data_plan.total_bytes <= data_plan.max_bytes ){
                return data_plan;
            }else{
                throw new System.ArgumentException("New site exceeds data plan limits. Reduce data use by deleting sites and or components.");
            }
        }

        public void RemoveSiteFromDataPlan(int admin_id){
            DataPlan admin_data_plan =_dbQuery.GetDataPlanByAdminId(admin_id);
            admin_data_plan.total_bytes -= this._site_container_size;
            _dbQuery.UpdateDataPlan( admin_data_plan );
        }

        //site components
         public DataPlan ValidateComponentAdditionForDataPlan(int admin_id, SiteComponent site_component){

            DataPlan data_plan =_dbQuery.GetDataPlanByAdminId(admin_id);
            data_plan.total_bytes += site_component.FindCharLength();
            
            if( data_plan.total_bytes <= data_plan.max_bytes ){
                return data_plan;
            }else{
                throw new System.ArgumentException($"New {site_component.GetType().Name.ToString()} exceeds data plan limits. Reduce data use by deleting sites and or components.");
            }
        }

        //does not remove component itself.
        public void RemoveFromDataPlan(SiteComponent site_component, int admin_id){
            DataPlan admin_data_plan =_dbQuery.GetDataPlanByAdminId(admin_id);
            admin_data_plan.total_bytes -= site_component.FindCharLength();
            _dbQuery.UpdateDataPlan( admin_data_plan );
        }

        public DataPlan ValidateDataPlanB(int admin_id, SiteComponent old_site_component, SiteComponent new_site_component){

            DataPlan data_plan = _dbQuery.GetDataPlanByAdminId(admin_id);
            //remove current component byte cost
            data_plan.total_bytes -= old_site_component.FindCharLength();

            //calculate new cost
            data_plan.total_bytes += new_site_component.FindCharLength();

            //compare
            if( data_plan.total_bytes <= data_plan.max_bytes ){
                return data_plan;
            }else{
                throw new System.ArgumentException("New component exceeds data plan limits.");
            }
        }

        public void UpdateDataPlan(DataPlan data_plan){
            _dbQuery.UpdateDataPlan( data_plan );
        }
    }
}