using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
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
        public ActionResult<Admin> Post([FromBody] Admin NewAdmin){
            dbContext.Add(NewAdmin);
            dbContext.SaveChanges();
            System.Console.WriteLine("%%%%%%%%%%%%posted%%%%%%%%%%%");
            Admin[] result = {NewAdmin}; 
            return NewAdmin;
        }

        [HttpDelete]
        public ActionResult<Admin> Destroy([FromBody] Admin TargetAdmin){
            Admin FoundAdmin = dbContext.Admins.SingleOrDefault(x => x.AdminId == TargetAdmin.AdminId);
            Admin r = FoundAdmin;
            dbContext.Remove( FoundAdmin );
            dbContext.SaveChanges();
            return r;
        }

        [HttpPut]
        public ActionResult<Admin> Update([FromBody] Admin TargetAdmin){
            Admin FoundAdmin = dbContext.Admins.FirstOrDefault(Admin => Admin.AdminId == TargetAdmin.AdminId);
            FoundAdmin.FirstName = TargetAdmin.FirstName;
            FoundAdmin.LastName = TargetAdmin.LastName;
            FoundAdmin.Email = TargetAdmin.Email; //must validate unique email
            FoundAdmin.Password = TargetAdmin.Password;
            FoundAdmin.UpdatedAt = DateTime.Now;
            dbContext.SaveChanges();
            return FoundAdmin;
        }
    }
}