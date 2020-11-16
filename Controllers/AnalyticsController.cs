using System.Threading.Tasks;
using dynamify.Controllers.ControllerMethods;
using dynamify.Models.AnalyticsModels;
using dynamify.Models.JsonModels;
using dynamify.ServerClasses.QueryClasses;
using Microsoft.AspNetCore.Mvc;


namespace dynamify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsController:ControllerBase
    {
        private AnalyticsQueries _dbQuery;
        private AnalyticsControllerMethods methods;
        public AnalyticsController(AnalyticsQueries dbQuery){
            _dbQuery = dbQuery;
            methods = new AnalyticsControllerMethods(dbQuery);
        }

        [HttpPost]
        [Route("create")]
        public ActionResult<ViewSession> GenUserSession([FromBody] ViewSession NewSession){
            return methods.genUserSessionMethod(NewSession);
        }

        [HttpPost]
        [Route("update")]
        public ActionResult<JsonResponse> UpdateSession([FromBody] ViewSession _CurrentSession){
            return methods.UpdateSessionMethod(_CurrentSession);
        }
    }
}