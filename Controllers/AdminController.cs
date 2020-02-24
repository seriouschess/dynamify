using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using dynamify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dynamify.Classes.Auth;
using dynamify.Models.QueryClasses;

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
        [Route("/[controller]/auth/generate")]
       
        public ActionResult<Token> GenAuth(){
            return authenticator.Generate();
        }

        [HttpGet] //get all for now
        public IEnumerable Get(){
            List<Admin> AllAdmins = adminQueries.All();
            System.Console.WriteLine(AllAdmins);
            IEnumerable results = AllAdmins.AsEnumerable();
            return results;
        }

        [HttpGet]
        [Route("/[controller]/by_email/{login_email}/{login_password}")]
        public ActionResult<Admin> LoginAdmin(string login_email, string login_password){
            return authenticator.RequestAdmin(login_email, login_password);
        }

        [HttpPost]
        public ActionResult<Admin> Post([FromBody] string _NewAdmin){
            Admin NewAdmin = JsonSerializer.Deserialize<Admin>(_NewAdmin);
            NewAdmin.token = authenticator.Generate().token;
            return adminQueries.SaveNewAdmin(NewAdmin);
        }

        [HttpDelete]
        [Route("/[controller]/{admin_id}")]
        public ActionResult<Admin> Destroy(int admin_id){
            System.Console.WriteLine("Admin Deleted >:O");
            Admin FoundAdmin = adminQueries.GetAdminById(admin_id);
            return FoundAdmin;
        }

        [HttpPut]
        public ActionResult<Admin> Update([FromBody] string _TargetAdmin){
            Admin TargetAdmin = JsonSerializer.Deserialize<Admin>(_TargetAdmin);
            Admin FoundAdmin = adminQueries.UpdateAdminById(TargetAdmin);
            
            return FoundAdmin;
        }
    }
}