using System.Collections;
using System.Collections.Generic;
using System.Linq;
using dynamify.Models;
using Microsoft.AspNetCore.Mvc;

namespace dynamify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController:Controller
    {
        private SiteContext dbContext;

        
        public HomeController(SiteContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public IEnumerable Get(){
            List<Admin> AllAdmins = dbContext.Admins.ToList();
            IEnumerable results = AllAdmins.AsEnumerable();
            return results;
        }

        // [HttpPost]
        // public IEnumerable Post(){

        // }
    }
}