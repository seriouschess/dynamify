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

        //Delete
        public Admin DeleteAdminById(int admin_id){
            Admin Subject = GetAdminById( admin_id );
            dbContext.Remove( Subject );
            dbContext.SaveChanges();
            return Subject;
        }

        //Update
        public Admin UpdateAdminById(Admin TargetAdmin){

            Admin AdminToUpdate = GetAdminById(TargetAdmin.admin_id);
            AdminToUpdate.first_name = TargetAdmin.first_name;
            AdminToUpdate.last_name = TargetAdmin.last_name;
            //email cannot be changed
            AdminToUpdate.password = TargetAdmin.password;
            AdminToUpdate.UpdatedAt = DateTime.Now;
            dbContext.SaveChanges();
            return AdminToUpdate;
        }
    }
}