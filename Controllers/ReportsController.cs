using dynamify.Controllers.ControllerMethods;
using dynamify.dtos;
using dynamify.Models.JsonModels;
using dynamify.ServerClasses.Email;
using dynamify.ServerClasses.QueryClasses;
using Microsoft.AspNetCore.Mvc;


namespace dynamify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController:ControllerBase
    {
        private Mailer _mailer;
        private ReportsControllerMethods _methods;
        public ReportsController(Mailer mailer, SiteQueries siteQueries, AdminQueries adminQueries){
            _mailer = mailer;
            _methods = new ReportsControllerMethods(mailer, siteQueries, adminQueries);
        }

        [HttpPost]
        [Route("feedback")]
        public JsonResponse PostFeedback(ContactForm new_contact){
            _mailer.SendFeedbackEmail( new_contact.email,new_contact.feedback );
            return new JsonSuccess("Feedback sent.");
        }

        [HttpGet]
        [Route("summary")]
        public JsonResponse SendSummaryReport(){
            _methods.SendSummaryReportMethod();
            return _methods.SendSummaryReportMethod();
        }
    }
}