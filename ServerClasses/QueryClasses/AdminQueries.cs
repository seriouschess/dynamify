using System;
using System.Collections.Generic;
using System.Linq;
using dynamify.Models;
using dynamify.Models.DataPlans;

//To assign max bytes
using dynamify.ServerClasses.DataLimiter;

namespace dynamify.ServerClasses.QueryClasses
{
    public class AdminQueries
    {
        private DatabaseContext dbContext;

        public AdminQueries(DatabaseContext _context){
            dbContext = _context;
        }

        //Read & Get
        public Admin GetAdminById(int admin_id){
            return dbContext.Admins.Where(x => x.admin_id == admin_id).Select(a => new Admin(){
                admin_id = a.admin_id,
                email = a.email,
                token = a.token,
                password = a.password,
                email_verified = a.email_verified
            }).SingleOrDefault();
        }

        //must be used when changing admin properties
        public Admin QueryAdminById(int admin_id){
            List<Admin> found_admins = dbContext.Admins.Where(x => x.admin_id == admin_id).ToList();
            if(found_admins.Count == 1){
                return found_admins[0];
            }else{
                throw new System.ArgumentException($"Cannot find admin ID: {admin_id}");
            }
        }

        public Admin GetAdminByEmail(string admin_email){
            List<Admin> FoundAdmins = dbContext.Admins.Where(x => x.email == admin_email).ToList();
            if(FoundAdmins.Count == 1){
                return FoundAdmins[0];
            }else{
                throw new System.ArgumentException($"Failed to find admin email {admin_email}");
            }
        }

        public List<Admin> GetAdminsByUsername(string username){
            List<Admin> FoundAdmins = dbContext.Admins.Where(x => x.username == username).ToList();
            return FoundAdmins;
        }

        public List<Admin> GetAdminsByEmail(string email){
            List<Admin> FoundAdmins = dbContext.Admins.Where(x => x.email == email).ToList();
            return FoundAdmins;
        }

        public List<Admin> All(){
            return dbContext.Admins.ToList();
        }

        //Create
        public Admin SaveNewAdmin(Admin NewAdmin){
            List<Admin> FoundAdmin = GetAdminsByEmail(NewAdmin.email);
            
            if(FoundAdmin.Count != 0){ //NewAdmin is unique
                throw new System.ArgumentException("Email exists! May not create more than one admin with existing email!"); 
            }

            FoundAdmin = GetAdminsByUsername(NewAdmin.username);

            if(FoundAdmin.Count != 0){
                throw new System.ArgumentException("Username exists! May not create more than one admin with existing username!");
            }

            dbContext.Add(NewAdmin);
            dbContext.SaveChanges();

            Admin Test = NewAdmin = dbContext.Admins.FirstOrDefault(x => x.email == NewAdmin.email);
            return Test;
        }

        public Admin SetVerifiedEmailAdmin(int admin_id, string admin_token){
            List<Admin> found_admins = dbContext.Admins.Where(x => x.admin_id == admin_id).ToList();
            if(found_admins.Count == 1){
                found_admins[0].email_verified = true;
            }else{
                throw new System.ArgumentException($"Admin ID: {admin_id} not found.");
            }
            dbContext.SaveChanges();
            return found_admins[0];
        }

        //Delete
        public Admin DeleteAdminById(int admin_id){
            Admin Subject = QueryAdminById( admin_id );
            dbContext.Remove( Subject );
            dbContext.SaveChanges();
            return Subject;
        }

        public void DeleteOutOfDateInvalidAdmins(){
            List<Admin> invalid_admins = dbContext.Admins.Where(x => x.email_verified == false).Where(x => x.CreatedAt.AddDays(1) < DateTime.Now).ToList();
            foreach(Admin admin in invalid_admins){
                dbContext.Remove(admin);
            }
            dbContext.SaveChanges();
        }

        //Update
        public Admin UpdateAdmin(Admin TargetAdmin){
            Admin SubjectAdmin = QueryAdminById(TargetAdmin.admin_id);
            
            if(TargetAdmin.username != null){
                SubjectAdmin.username = TargetAdmin.username;
            }

            //email cannot be changed

            SubjectAdmin.UpdatedAt = DateTime.Now;
            dbContext.SaveChanges();
            return SubjectAdmin;
        }

        public Admin UpdateAdminPassword(int admin_id, string new_hashed_password){
            Admin FoundAdmin = QueryAdminById(admin_id);
            FoundAdmin.password = new_hashed_password;
            FoundAdmin.UpdatedAt = DateTime.Now;
            dbContext.SaveChanges();
            return FoundAdmin;
        }

        //DataPlans
        public DataPlan GetDataPlanByAdminId(int admin_id){
            List<DataPlan> found_plans = dbContext.DataPlans.Where(x => x.admin_id == admin_id).ToList();
            if(found_plans.Count == 0){
                return null;
            }else if(found_plans.Count == 1){
                return found_plans[0];
            }else{
                throw new System.Exception($"More than one Data Plan exists for admin_id {admin_id}");
            }
        }

        public DataPlan CreateNewDataPlan(int admin_id){
            if(GetDataPlanByAdminId(admin_id) == null){
                DataPlan new_data_plan = new FreeDataPlan();
                new_data_plan.admin_id = admin_id;
                new_data_plan.total_bytes = 0;
                dbContext.Add(new_data_plan);
                dbContext.SaveChanges();
                return new_data_plan;
            }else{
                throw new System.ArgumentException($"Data Plan already exists for admin id {admin_id}");
            }
        }

        public DataPlan UpdateDataPlan( DataPlan updated_data_plan ){
            DataPlan data_plan = GetDataPlanByAdminId(updated_data_plan.admin_id);
            data_plan.total_bytes = updated_data_plan.total_bytes;
            dbContext.SaveChanges();
            return data_plan;
        }

        public int GetSiteCountForAdminId( int admin_id ){
            int site_count = dbContext.Sites.Where(x => x.admin_id == admin_id).Count();
            return site_count;
        }
    }
}