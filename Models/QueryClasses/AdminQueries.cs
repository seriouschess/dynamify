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

        public Admin loginAdmin(string email){
            List<Admin> FoundAdmin = dbContext.Admins.Where(x => x.email == email).ToList();
            if(FoundAdmin.Count > 0){ 
                if(FoundAdmin.Count == 1){ //unique email Admin found.
                    return FoundAdmin[0];
                }else{ //FoundAdmin.Count > 1
                    Admin ErrorAdmin = new Admin();
                    ErrorAdmin.first_name = "<DUPLICATE, More than one admin with the same email exists!>";
                    ErrorAdmin.last_name = "<DUPLICATE, More than one admin with the same email exists!>";
                    ErrorAdmin.email = "<DUPLICATE, More than one admin with the same email exists!>";
                    ErrorAdmin.password = "<DUPLICATE, More than one admin with the same email exists!>";
                    return ErrorAdmin; 
                }
                
            }else{
                Admin ErrorAdmin = new Admin();
                ErrorAdmin.first_name = "<NOT FOUND, Email invalid>";
                ErrorAdmin.last_name = "<NOT FOUND, Email invalid>";
                ErrorAdmin.email = "<NOT FOUND, Email invalid>";
                ErrorAdmin.password = "<NOT FOUND, Email invalid>";
                return ErrorAdmin; 
            }
        }

        public Admin GetAdminByLogin(string email, string password){ //single login method for now, may be split later
            Admin FoundAdmin = loginAdmin(email);
            
            //validate email and password
                if(FoundAdmin.password == password){
                    //success
                    return FoundAdmin;
                }else{
                    Admin ErrorAdmin = new Admin();
                    ErrorAdmin.first_name = "<ACCESS DENIED, Password Invalid>";
                    ErrorAdmin.last_name = "<ACCESS DENIED, Password Invalid>";
                    ErrorAdmin.email = "<ACCESS DENIED, Password Invalid>";
                    ErrorAdmin.password = "<ACCESS DENIED, Password Invalid>";
                    return ErrorAdmin;
                }
        }

        public List<Admin> All(){
            return dbContext.Admins.ToList();
        }


        //Create
        public Admin SaveNewAdmin(Admin NewAdmin){
            Admin validation_query = loginAdmin(NewAdmin.email);

            dbContext.Add(NewAdmin);
            dbContext.SaveChanges();

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