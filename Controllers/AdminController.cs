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
        public ActionResult<Admin> Post([FromBody] AdminRegistrationDto NewAdmin){
           return methods.PostMethod(NewAdmin);
        }

        //not currently used
        [HttpPost]
        [Route("delete")]
        public JsonResponse Delete([FromBody] AdminRequestDto request){
            return methods.DeleteMethod(request);
        }

        //not currently used, antiquated method
        [HttpPut] 
        public ActionResult<Admin> Update([FromBody] string _TargetAdmin){
            return methods.UpdateMethod(_TargetAdmin);
        }

        [HttpGet] //used for testing
        public IEnumerable Get(){
            return methods.GetAllMethod();
        }

        [HttpGet]
        [Route("test")]
        public string Test(){
            return methods.TestMethod();
        }

        [HttpGet]
        [Route("test2")]
        public string Test2(){
            string input = "$2a$13$/8Wncr26eAmxD1l6cAF9FuejrBm3X3XqSSOPLW11Jxsr1X.LNVnAm";
            return methods.TestMethodTwo(input);
        }
    }
}