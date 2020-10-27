//asp.net dependencies
using System.Collections;
using Microsoft.AspNetCore.Mvc;

//project dependencies
using dynamify.Classes.Auth;

//models
using dynamify.Models;
using dynamify.dtos;
using dynamify.Models.JsonModels;
using dynamify.Controllers.ControllerMethods;
using System.Threading.Tasks;
using dynamify.ServerClasses.QueryClasses;

namespace dynamify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private AdminQueries adminQueries;
        private AdminControllerMethods methods;
        public AdminController(AdminQueries _adminQueries)
        {
            adminQueries = _adminQueries;
            methods = new AdminControllerMethods(adminQueries);
        }

        [HttpGet]
        [Route("email/password_reset/send/{email}")]
        public async Task<ActionResult<JsonResponse>> SendPasswordResetEmail(string email){
            return await methods.SendPasswordResetEmailMethod(email);
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
            return methods.UpdateMethod(TargetAdmin);
        }

        [HttpPut]
        [Route("password/reset/{admin_email}/{admin_token}/{new_password}")]
        public ActionResult<Admin> UpdatePassword(string admin_email, string admin_token, string new_password){
            return methods.UpdatePasswordMethod(admin_email, admin_token, new_password);
        }
    }
}