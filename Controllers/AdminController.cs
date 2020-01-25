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

        [HttpPost]
        public ActionResult<Admin> Post([FromBody] string _NewAdmin){
            Admin NewAdmin = JsonSerializer.Deserialize<Admin>(_NewAdmin);
            dbContext.Add(NewAdmin);
            dbContext.SaveChanges();
            Admin queryAdmin = new Admin();
            queryAdmin.email = NewAdmin.email;
            queryAdmin = dbContext.Admins.FirstOrDefault(x => x.email == queryAdmin.email);
            return queryAdmin;
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