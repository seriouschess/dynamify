using System;
using dynamify.dtos;
using dynamify.Models.JsonModels;
using dynamify.Models.SiteModels;
using dynamify.ServerClasses.Email;
using Microsoft.AspNetCore.Mvc;


namespace dynamify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController:ControllerBase
    {
        private Mailer _mailer;
        public ReportsController(Mailer mailer){
            _mailer = mailer;
        }

        [HttpPost]
        [Route("feedback")]
        public JsonResponse PostFeedback(ContactForm new_contact){
            _mailer.SendFeedbackEmail( new_contact.email,new_contact.feedback );
            return new JsonSuccess("Feedback sent.");
        }
    }
}