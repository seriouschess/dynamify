//asp.net dependencies
using System.Text.Json;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

//project dependencies
using dynamify.Classes.Auth;
using dynamify.ServerClasses.Validators;
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

        public AdminControllerMethods(AdminQueries _dbQuery){
            dbQuery = _dbQuery;
            authenticator = new Auth(_dbQuery);
            validator = new RegistrationValidator(_dbQuery);
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
                dbQuery.SaveNewAdmin(NewAdmin); //create admin
                return authenticator.ValidateAdmin(NewAdmin.email, unhashed_password); //login admin
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

        public IEnumerable GetAllMethod(){
            List<Admin> AllAdmins = dbQuery.All();
            System.Console.WriteLine(AllAdmins);
            IEnumerable results = AllAdmins.AsEnumerable();
            return results;
        }

        public async Task<string> TestMethod(){
            Mailer Mail = new Mailer();
            await Mail.SendPasswordResetMail("bigchunk1@comcast.net", "SomeRandomString");
            return "mail sent!";
        }
    }
}