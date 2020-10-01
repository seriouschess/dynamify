using System;
using System.Collections.Generic;
using System.Linq;
using dynamify.Models;

namespace dynamify.Models.QueryClasses
{
    public class AdminQueries
    {
        private MyContext dbContext;

        public AdminQueries(MyContext _context){
            dbContext = _context;
        }

        //Read & Get
        public Admin GetAdminById(int admin_id){
            return dbContext.Admins.SingleOrDefault(x => x.admin_id == admin_id);
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
                ErrorAdmin.first_name = error_msg;
                ErrorAdmin.last_name = error_msg;
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
            Admin Subject = GetAdminById( admin_id );
            dbContext.Remove( Subject );
            dbContext.SaveChanges();
            return Subject;
        }

        //Update
        public Admin UpdateAdmin(Admin TargetAdmin){
            Admin SubjectAdmin = GetAdminById(TargetAdmin.admin_id);
            
            if(TargetAdmin.first_name != null){
                SubjectAdmin.first_name = TargetAdmin.first_name;
            }

            if(TargetAdmin.last_name != null){
                SubjectAdmin.last_name = TargetAdmin.last_name;
            }

            //email cannot be changed

            SubjectAdmin.UpdatedAt = DateTime.Now;
            dbContext.SaveChanges();
            return SubjectAdmin;
        }

        public Admin UpdateAdminPassword(int admin_id, string new_password){
            Admin FoundAdmin = GetAdminById(admin_id);
            FoundAdmin.password = new_password;
            FoundAdmin.UpdatedAt = DateTime.Now;
            dbContext.SaveChanges();
            return FoundAdmin;
        }
    }
}