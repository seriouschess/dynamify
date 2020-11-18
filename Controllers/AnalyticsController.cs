using System.Threading.Tasks;
using dynamify.Controllers.ControllerMethods;
using dynamify.Models.AnalyticsModels;
using dynamify.Models.JsonModels;
using dynamify.ServerClasses.Analytics.dtos;
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

        [HttpGet]
        [Route("by_site_id/{site_id}")]
        public ActionResult<SiteViewDataDto> GetAnalyticsForSiteById(int site_id ){
            return methods.GetAnalyticsForSiteByIdMethod(site_id);
        }

        [HttpGet]
        [Route("for_site_url/{site_url}")]
        public ActionResult<SiteViewDataDto> GetAnalyticsForSiteByURL(string site_url){
            return methods.GetAnalyticsForSiteByURLMethod(site_url);
        }
    }
}