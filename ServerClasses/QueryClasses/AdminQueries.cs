using System;
using System.Collections.Generic;
using System.Linq;
using dynamify.Models;

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
                email_verified = a.email_verified,
                data_plans = dbContext.DataPlans.Where(x => x.admin_id == admin_id).ToList()
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

        public List<Admin> GetAdminsByEmail(string email){
            List<Admin> FoundAdmins = dbContext.Admins.Where(x => x.email == email).ToList();
            return FoundAdmins;
        }

        public List<Admin> All(){
            return dbContext.Admins.ToList();
        }

        //Create
        public Admin SaveNewAdmin(Admin NewAdmin){
            List<Admin> FoundAdmin = dbContext.Admins.Where(x => x.email == NewAdmin.email).ToList();
            
            if(FoundAdmin.Count == 0){ //NewAdmin is unique
                dbContext.Add(NewAdmin);
                dbContext.SaveChanges();
            }else{
                Admin ErrorAdmin = new Admin();
                string error_msg = "<Error: email exists!, May not create more than one admin with existing email!>";
                ErrorAdmin.username = error_msg;
                ErrorAdmin.email = error_msg;
                ErrorAdmin.password = error_msg;
                return ErrorAdmin; 
            }

            Admin Test = NewAdmin = dbContext.Admins.FirstOrDefault(x => x.email == NewAdmin.email);
            return Test;
        }

        public Admin SetValidEmailAdmin(string admin_email, string admin_token){
            List<Admin> found_admins = GetAdminsByEmail(admin_email);
            if( found_admins.Count == 1 ){
                Admin found_admin = found_admins[0];
                found_admin.email_verified = true;
                dbContext.SaveChanges();
                return found_admin;
            }else{
                throw new ArgumentException($"Failed to find admin email {admin_email}.");
            }
        }

        //Delete
        public Admin DeleteAdminById(int admin_id){
            Admin Subject = QueryAdminById( admin_id );
            dbContext.Remove( Subject );
            dbContext.SaveChanges();
            return Subject;
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
        public DataPlan FindDataPlanByAdminId(int admin_id){
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
            if(FindDataPlanByAdminId(admin_id) == null){
                DataPlan new_data_plan = new DataPlan();
                new_data_plan.admin_id = admin_id;
                new_data_plan.total_bytes = 0;
                new_data_plan.max_bytes = 1000000*10; //10MB
                new_data_plan.premium_tier = 0;
                dbContext.Add(new_data_plan);
                dbContext.SaveChanges();
                return new_data_plan;
            }else{
                throw new System.ArgumentException($"Data Plan already exists for admin id {admin_id}");
            }
        }

        public DataPlan UpdateDataPlan( DataPlan updated_data_plan ){
            DataPlan data_plan = FindDataPlanByAdminId(updated_data_plan.admin_id);
            data_plan.total_bytes = updated_data_plan.total_bytes;
            data_plan.max_bytes = updated_data_plan.max_bytes;
            dbContext.SaveChanges();
            return data_plan;
        }
    }
}