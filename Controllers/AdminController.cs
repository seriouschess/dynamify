using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using dynamify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dynamify.Classes.Auth;
using dynamify.Models.QueryClasses;
using dynamify.dtos;
using dynamify.Models.JsonModels;

namespace dynamify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private AdminQueries adminQueries;

        private readonly ILogger<AdminController> _logger;
        private Auth authenticator;

        public AdminController(ILogger<AdminController> logger, AdminQueries _adminQueries)
        {
            _logger = logger;
            adminQueries = _adminQueries;
            authenticator = new Auth(adminQueries);
        }

        [HttpGet]
        [Route("auth/generate")]
        public ActionResult<Token> GenAuth(){
            return authenticator.Generate();
        }

        [HttpGet] //used for testing
        public IEnumerable Get(){
            List<Admin> AllAdmins = adminQueries.All();
            System.Console.WriteLine(AllAdmins);
            IEnumerable results = AllAdmins.AsEnumerable();
            return results;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<Admin> LoginAdmin([FromBody] LoginDto LoginInfo ){
            return authenticator.RequestAdmin(LoginInfo.email, LoginInfo.password);
        }

        
        [HttpPost]
        public ActionResult<Admin> Post([FromBody] AdminRegistrationDto NewAdmin){
            Admin _NewAdmin = new Admin();
            _NewAdmin.first_name = NewAdmin.first_name;
            _NewAdmin.last_name = NewAdmin.last_name;
            _NewAdmin.email = NewAdmin.email;
            _NewAdmin.password = NewAdmin.password;
            _NewAdmin.token = authenticator.Generate().token;
            adminQueries.SaveNewAdmin(_NewAdmin);
            return authenticator.RequestAdmin(NewAdmin.email, NewAdmin.password);
        }


        //not currently used
        [HttpPost]
        [Route("delete")]
        public JsonResponse Delete([FromBody] AdminRequestDto request){
            if(authenticator.VerifyAdmin(request.admin_id, request.token)){
                Admin FoundAdmin = adminQueries.GetAdminById(request.admin_id);
                if(FoundAdmin.admin_id == request.admin_id){
                    adminQueries.DeleteAdminById(request.admin_id);
                    return new JsonSuccess("Admin sucessfully deleted. I hope that's what you wanted.");
                }else{
                    return new JsonFailure("Admin not found.");
                }
            }else{
                return new JsonFailure("Invalid token. Stranger danger.");
            }
        }

        //not currently used
        [HttpPut] 
        public ActionResult<Admin> Update([FromBody] string _TargetAdmin){
            Admin TargetAdmin = JsonSerializer.Deserialize<Admin>(_TargetAdmin);
            Admin FoundAdmin = adminQueries.UpdateAdminById(TargetAdmin);
            return FoundAdmin;
        }
    }
}