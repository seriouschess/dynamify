//asp.net dependencies
using System.Text.Json;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

//project dependencies
using dynamify.Classes.Auth;
using dynamify.Models.QueryClasses;

//models
using dynamify.Models;
using dynamify.dtos;
using dynamify.Models.JsonModels;

namespace dynamify.Controllers.ControllerMethods
{
    public class AdminControllerMethods
    {
        private AdminQueries dbQuery;

        private Auth authenticator;

        public AdminControllerMethods(AdminQueries _dbQuery){
            dbQuery = _dbQuery;
            authenticator = new Auth(_dbQuery);
        }

        public Admin LoginAdminMethod(LoginDto LoginInfo){
            return authenticator.ValidateAdmin(LoginInfo.email, LoginInfo.password);
        }

        public Admin PostMethod(AdminRegistrationDto NewAdmin){
             Admin _NewAdmin = new Admin();
            _NewAdmin.first_name = NewAdmin.first_name;
            _NewAdmin.last_name = NewAdmin.last_name;
            _NewAdmin.email = NewAdmin.email;
            _NewAdmin.password = authenticator.HashString(NewAdmin.password);
            _NewAdmin.token = authenticator.Generate().token;
            dbQuery.SaveNewAdmin(_NewAdmin);
            return authenticator.ValidateAdmin(NewAdmin.email, NewAdmin.password);
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

        public Admin UpdateMethod(string _TargetAdmin ){
            Admin TargetAdmin = JsonSerializer.Deserialize<Admin>(_TargetAdmin);
            Admin FoundAdmin = dbQuery.UpdateAdminById(TargetAdmin);
            return FoundAdmin;
        }

        public IEnumerable GetAllMethod(){
            List<Admin> AllAdmins = dbQuery.All();
            System.Console.WriteLine(AllAdmins);
            IEnumerable results = AllAdmins.AsEnumerable();
            return results;
        }

        public string TestMethod(){
            string final = authenticator.HashString("hi");
            return final;
        }

        public string TestMethodTwo(string input){
            bool result = authenticator.VerifyHash("hi", input);
            return $"Result {result}";
        }
    }
}