using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.Json;
using dynamify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dynamify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private MyContext dbContext;

        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger, MyContext context)
        {
            _logger = logger;
            dbContext = context;
        }

        [HttpGet] //get all for now
        public IEnumerable Get(){
            List<Admin> AllAdmins = dbContext.Admins.ToList();
            System.Console.WriteLine(AllAdmins);
            IEnumerable results = AllAdmins.AsEnumerable();
            return results;
        }

        [HttpGet]
        [Route("/[controller]/by_email/{login_email}/{login_password}")]
        public ActionResult<Admin> GetByEmail(string login_email, string login_password){
            Login LoginModel = new Login();
            LoginModel.email = login_email;
            LoginModel.password = login_password;

            //validate email and password
            List<Admin> FoundAdmin = dbContext.Admins.Where(x => x.email == LoginModel.email).ToList();
            if(FoundAdmin.Count == 1){
                if(FoundAdmin[0].password == LoginModel.password){
                    //success
                    return FoundAdmin[0];
                }else{
                    Admin ErrorAdmin = new Admin();
                    ErrorAdmin.first_name = "<ACCESS DENIED, Password Invalid>";
                    ErrorAdmin.last_name = "<ACCESS DENIED, Password Invalid>";
                    ErrorAdmin.email = "<ACCESS DENIED, Password Invalid>";
                    ErrorAdmin.password = "<ACCESS DENIED, Password Invalid>";
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

        [HttpPost]
        public ActionResult<Admin> Post([FromBody] string _NewAdmin){
            Admin NewAdmin = JsonSerializer.Deserialize<Admin>(_NewAdmin);
            List<Admin> validation_query = dbContext.Admins.Where(x => x.email == NewAdmin.email).ToList();
            if(validation_query.Count != 0){
                NewAdmin.first_name = "<NOT ENTERED, Duplicate email>";
                NewAdmin.last_name = "<NOT_ENTERED, Duplicate email>";
                NewAdmin.email = "<NOT_ENTERED, Duplicate email>";
                NewAdmin.password = "<NOT_ENTERED, Duplicate email>";
                return NewAdmin;
            }else{
                dbContext.Add(NewAdmin);
                dbContext.SaveChanges();
                Admin queryAdmin = new Admin();
                queryAdmin.email = NewAdmin.email;
                queryAdmin = dbContext.Admins.FirstOrDefault(x => x.email == NewAdmin.email);
                return queryAdmin;
            }
        }

        [HttpDelete]
        [Route("/[controller]/{admin_id}")]
        public ActionResult<Admin> Destroy(int admin_id){
            System.Console.WriteLine("Admin Deleted >:O");
            Admin FoundAdmin = dbContext.Admins.SingleOrDefault(x => x.admin_id == admin_id);
            dbContext.Remove( FoundAdmin );
            dbContext.SaveChanges();
            return FoundAdmin;
        }

        [HttpPut]
        public ActionResult<Admin> Update([FromBody] string _TargetAdmin){
            Admin TargetAdmin = JsonSerializer.Deserialize<Admin>(_TargetAdmin);
            Admin FoundAdmin = dbContext.Admins.FirstOrDefault(Admin => Admin.admin_id == TargetAdmin.admin_id);
            FoundAdmin.first_name = TargetAdmin.first_name;
            FoundAdmin.last_name = TargetAdmin.last_name;
            //email cannot be changed
            FoundAdmin.password = TargetAdmin.password;
            FoundAdmin.UpdatedAt = DateTime.Now;
            dbContext.SaveChanges();
            return FoundAdmin;
        }
    }
}