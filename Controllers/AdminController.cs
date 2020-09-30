//asp.net dependencies
using System.Collections;
using Microsoft.AspNetCore.Mvc;

//project dependencies
using dynamify.Classes.Auth;
using dynamify.Models.QueryClasses;

//models
using dynamify.Models;
using dynamify.dtos;
using dynamify.Models.JsonModels;
using dynamify.Controllers.ControllerMethods;
using System.Threading.Tasks;

namespace dynamify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private AdminQueries adminQueries;
        private Auth authenticator;
        private AdminControllerMethods methods;
        public AdminController(AdminQueries _adminQueries)
        {
            adminQueries = _adminQueries;
            authenticator = new Auth(adminQueries);
            methods = new AdminControllerMethods(adminQueries);
        }

        [HttpGet]
        [Route("auth/generate")]
        public ActionResult<Token> GenAuth(){
            return authenticator.Generate();
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<Admin> LoginAdmin([FromBody] LoginDto LoginInfo ){
            return methods.LoginAdminMethod(LoginInfo);
        }

        [HttpPost]
        [Route("new")]
        public ActionResult<Admin> Register([FromBody] AdminRegistrationDto NewAdmin){
           return methods.RegisterMethod(NewAdmin);
        }

        [HttpPut]
        [Route("activate/{admin_email}/{admin_token}")]
        public ActionResult<Admin> ActivateValidEmail(string admin_email, string admin_token){
            return methods.VerifyEmailForAdmin(admin_email, admin_token);
        }

        [HttpDelete]
        [Route("delete")]
        public JsonResponse Delete([FromBody] AdminRequestDto request){
            return methods.DeleteMethod(request);
        }

        [HttpPut] 
        public ActionResult<Admin> Update([FromBody] Admin TargetAdmin){
            System.Console.WriteLine($"Updating admin Id:{TargetAdmin.admin_id}");
            return methods.UpdateMethod(TargetAdmin);
        }

        [HttpGet]
        [Route("test")]
        public Task<string> Test(){
            System.Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            return methods.TestMethod();
        }
    }
}