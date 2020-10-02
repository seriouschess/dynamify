//asp.net dependencies
using System.Text.Json;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

//project dependencies
using dynamify.Classes.Auth;
using dynamify.ServerClasses.Auth;
using dynamify.Models.QueryClasses;

//models
using dynamify.Models;
using dynamify.dtos;
using dynamify.Models.JsonModels;
using dynamify.Email;
using System.Threading.Tasks;

namespace dynamify.Controllers.ControllerMethods
{
    public class AdminControllerMethods
    {
        private AdminQueries dbQuery;

        private Auth authenticator;

        private RegistrationValidator validator;

        private Mailer mailer;

        public AdminControllerMethods(AdminQueries _dbQuery){
            dbQuery = _dbQuery;
            authenticator = new Auth(_dbQuery);
            validator = new RegistrationValidator(_dbQuery);
            mailer = new Mailer();
        }

        public Admin LoginAdminMethod(LoginDto LoginInfo){
            return authenticator.ValidateAdmin(LoginInfo.email, LoginInfo.password);
        }

        public Admin RegisterMethod(AdminRegistrationDto _NewAdmin){
             Admin NewAdmin = new Admin();

                NewAdmin.first_name = _NewAdmin.first_name;
                NewAdmin.last_name = _NewAdmin.last_name;
                NewAdmin.email = _NewAdmin.email;
                NewAdmin.password = _NewAdmin.password;
                NewAdmin.token = authenticator.Generate().token;
                string verdict = validator.ValidateAdmin(NewAdmin);
             if(verdict == "pass"){
                string unhashed_password = _NewAdmin.password; //for the first login 
                NewAdmin.password = authenticator.HashString(_NewAdmin.password);
                Admin RegisteredAdmin = dbQuery.SaveNewAdmin(NewAdmin); //create admin

                //send validation email
                mailer.SendRegistrationConfirmationEmail(RegisteredAdmin.email, RegisteredAdmin.token);

                return authenticator.ValidateAdmin(NewAdmin.email, unhashed_password); //email not yet validated!
             }else if( verdict == "invalid credentials"){
                 string message = "< Error: Invalid Registration >";

                Admin blank_admin = new Admin();
                blank_admin.first_name = message;
                blank_admin.last_name = message;
                blank_admin.email = message;
                blank_admin.password = message;
                blank_admin.token = "XXX";

                return blank_admin;
             }else{
                string message = "< Error: Duplicate Email >";

                Admin blank_admin = new Admin();
                blank_admin.first_name = message;
                blank_admin.last_name = message;
                blank_admin.email = message;
                blank_admin.password = message;
                blank_admin.token = "XXX";

                return blank_admin;
             }
        }

        public Admin VerifyEmailForAdmin(string admin_email, string admin_token){
            if( authenticator.VerifyAdminForEmailValidation(admin_email, admin_token) ){
                return dbQuery.SetValidEmailAdmin(admin_email, admin_token);
            }else{
                throw new System.ArgumentException("Invalid credentials");
            }
        }

        public async Task<JsonResponse> SendPasswordResetEmailMethod(string requested_mail){
            Admin FoundAdmin = dbQuery.GetAdminByEmail(requested_mail);
            await mailer.SendPasswordResetMail(FoundAdmin.email, FoundAdmin.token);
            return new JsonSuccess("Password verification email sent.");
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

        public IEnumerable GetAllMethod(){
            List<Admin> AllAdmins = dbQuery.All();
            System.Console.WriteLine(AllAdmins);
            IEnumerable results = AllAdmins.AsEnumerable();
            return results;
        }

        // public async Task<string> TestMethod(){
        //     Mailer Mail = new Mailer();
        //     await Mail.SendPasswordResetMail("SOME_EMAIL :D", "SomeRandomString");
        //     return "mail sent!";
        // }
    }
}