//asp.net dependencies
using System.Text.Json;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

//project dependencies
using dynamify.Classes.Auth;
using dynamify.ServerClasses.Auth;

//models
using dynamify.Models;
using dynamify.dtos;
using dynamify.Models.JsonModels;
using System.Threading.Tasks;
using dynamify.ServerClasses.QueryClasses;
using dynamify.ServerClasses.Email;
using Microsoft.AspNetCore.Mvc;

namespace dynamify.Controllers.ControllerMethods
{
    public class AdminControllerMethods: ControllerBase
    {
        private AdminQueries dbQuery;

        private Auth authenticator;

        private TokenGenerator _tGen;

        private RegistrationValidator validator;

        private Mailer mailer;

        public AdminControllerMethods(AdminQueries _dbQuery, Mailer _mailer){
            dbQuery = _dbQuery;
            mailer = _mailer;
            authenticator = new Auth(_dbQuery);
            validator = new RegistrationValidator(_dbQuery);
            _tGen = new TokenGenerator();
        }

        public ActionResult<Admin> LoginAdminMethod(LoginDto LoginInfo){
            try{
                return authenticator.ValidateAdmin(LoginInfo.email, LoginInfo.password);
            }catch(System.ArgumentException e){
                return StatusCode(400, e.Message);
            }
        }

        public ActionResult<Admin> RegisterMethod(AdminRegistrationDto _NewAdmin){

                Admin NewAdmin = new Admin(){
                    username = _NewAdmin.username,
                    email = _NewAdmin.email,
                    password = _NewAdmin.password,
                    token = _tGen.GenerateToken()
                };
                
                string verdict = validator.ValidateAdmin(NewAdmin);
                //authenticator.ValidateAdmin(NewAdmin.email, unhashed_password); not used

             if(verdict == "pass"){

                //hash password
                string unhashed_password = _NewAdmin.password; //for the first login 
                NewAdmin.password = authenticator.HashString(_NewAdmin.password);
                Admin RegisteredAdmin = dbQuery.SaveNewAdmin(NewAdmin); //create admin
                dbQuery.CreateNewDataPlan(RegisteredAdmin.admin_id); //create data plan for admin

                //send validation email
                mailer.SendRegistrationConfirmationEmail(RegisteredAdmin.email, RegisteredAdmin.token);

                return RegisteredAdmin;
             }else if( verdict == "invalid credentials"){
                return StatusCode(400, "Invalid Registration" );

             }else{
                return StatusCode(400, "Duplicate Email, use an Email that has not been taken.");
             }
        }

        public ActionResult<Admin> VerifyEmailForAdmin(string admin_email, string admin_token){
            if( authenticator.VerifyAdminForEmailValidation(admin_email, admin_token) ){
                return dbQuery.SetValidEmailAdmin(admin_email, admin_token);
            }else{
                return StatusCode(400, "Invalid credentials");
            }
        }

        public async Task<ActionResult<JsonResponse>> SendPasswordResetEmailMethod(string requested_mail){
            try{
                Admin FoundAdmin = dbQuery.GetAdminByEmail(requested_mail);
                await mailer.SendPasswordResetMail(FoundAdmin.email, FoundAdmin.token);
                return new JsonSuccess("Password verification email sent.");
            }catch{
                return StatusCode(400, "Email not found");
            }
        }

        public JsonResponse DeleteMethod(AdminRequestDto request){
            if(authenticator.VerifyAdmin(request.admin_id, request.token)){
                Admin FoundAdmin = dbQuery.GetAdminById(request.admin_id);
                if(FoundAdmin.admin_id == request.admin_id){
                    dbQuery.DeleteAdminById(request.admin_id);
                    return new JsonSuccess("Admin sucessfully deleted. I hope that's what you wanted.");
                }else{
                    return new JsonFailure("Admin not found.");
                }
            }else{
                return new JsonFailure("Invalid token. Stranger danger.");
            }
        }

        public Admin UpdateMethod(Admin TargetAdmin ){
            Admin FoundAdmin = dbQuery.UpdateAdmin(TargetAdmin);
            return FoundAdmin;
        }

        public Admin UpdatePasswordMethod(string admin_email, string admin_token, string new_password){
            if( authenticator.VerifyAdminForEmailValidation(admin_email, admin_token) ){
                Admin FoundAdmin = dbQuery.GetAdminByEmail(admin_email);
                string password_hash = authenticator.HashString(new_password);
                return dbQuery.UpdateAdminPassword(FoundAdmin.admin_id, password_hash);
            }else{
                throw new System.ArgumentException("Invalid credentials");
            }
        }
    }
}