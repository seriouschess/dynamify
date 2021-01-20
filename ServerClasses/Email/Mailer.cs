using System.Net.Mail;
using System.Net;
using System;
using System.Threading.Tasks;

using dynamify.Configuration;
using dynamify.dtos;

namespace dynamify.ServerClasses.Email
{
    public class Mailer
    {
        String FROM;
        String FROMNAME;                       
        String SMTP_USERNAME;           
        String SMTP_PASSWORD;                      
        String HOST;
        int PORT;
        
        public Mailer(){
            FROM = ConfSettings.Configuration["EmailConfig:FROM"];
            FROMNAME = ConfSettings.Configuration["EmailConfig:FROMNAME"];
            SMTP_USERNAME = ConfSettings.Configuration["EmailConfig:SMTP_USERNAME"];
            SMTP_PASSWORD = ConfSettings.Configuration["EmailConfig:SMTP_PASSWORD"];
            HOST = ConfSettings.Configuration["EmailConfig:HOST"];
            PORT = Int32.Parse(ConfSettings.Configuration["EmailConfig:PORT"]);
        }

        public String CreateRegistrationMailBody(int admin_id, string token, string domain){
            String new_body = "<h1>Thank you for registering to siteleaves.com</h1>" +
            "<p>You may now click the following link to activate your account:</p>" +
            $"<p>Get started by clicking <a href='http://{domain}/app/activate/{admin_id}/{token}'>this link</a>.</p>"+
            $"<br> This email was sent automatically as a direct result of a registration action"+
            "<p>If you happen to have gotten this email by mistake, please send a reply to this email to give feedback to the admin and we will do our best to apprehend the traitors.</p>";
            return new_body; 
        }

        public String CreatePasswordResetMailBody(int admin_id, string token, string domain){
            String new_body = "<h2>Site Leaves account password reset</h2>" +
            "<p>You've requested to reset your account password. We've got you!</p>" +
            "<p>Click the following link to reset your password:</p>" +
            $"<p><a href='http://{domain}/app/password/reset/{admin_id}/{token}'>Click Here</a></p>";
            return new_body;
        }

        public String CreateFeedbackMailBody(string email, string feedback){
            String new_body = "<h2>Someone has left feedback on siteleaves.com</h2>" +
            $"<p>{feedback}</p>";

            if(email != null && email != ""){
                new_body += $"<p>They left an email at {email}</p>";
            }else{
                new_body += "<p>They did not leave an email.</p>";
            }
            return new_body;
        }

        public String CreateSummaryReportEmailBody(SummaryReportDto report_data){
            double rounded_total_megabytes = Math.Round(report_data.total_storage_megabytes, 2);
            String new_body = $"<h2> SiteLeaves Summary Data for { DateTime.Now.ToLongDateString() } </h2>" +
            $"<p>Total Admins Registered: {report_data.total_admins}</p>" +
            $"<p>Total New Admins This month: {report_data.total_new_admins_this_month}</p>" +
            $"<p>Total Sites Created: {report_data.total_sites} </p>" +
            $"<p>Total New Sites Created This month: {report_data.total_new_sites_this_month}</p>" +
            $"<p> Total megabytes used across all users: { rounded_total_megabytes }  </p>";
            return new_body;
        }

        public async Task<string> SendMail(String recipient_address, String subject, String body){

            //Configure message content
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(FROM, FROMNAME);
            message.To.Add(new MailAddress(recipient_address));
            message.Subject = subject;
            message.Body = body;

            using (var client = new System.Net.Mail.SmtpClient(HOST, PORT))
            {
                // Pass SMTP credentials
                client.Credentials =
                    new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);                

                client.EnableSsl = true;                // Enable SSL encryption

                try
                {
                    await Task.Run(() => client.Send(message));
                    return "Email sent!";
                }
                catch (Exception ex)
                {
                    throw new System.Exception("The Email was not sent." + " Error message: " + ex.Message);
                }
            }
        }

        public async Task<string> SendPasswordResetMail(string user_email, int admin_id, string new_token, string domain){
            String mail_content = CreatePasswordResetMailBody(admin_id, new_token, domain);
            String mail_title = "Site Leaves account password reset";
            return await SendMail(user_email, mail_title, mail_content);
        }

        public void SendRegistrationConfirmationEmail(string recipient_address, int admin_id, string password, string domain){
            String mail_content = CreateRegistrationMailBody(admin_id, password, domain);
            String mail_title = "Site Leaves Registration confirmation";
            Task.Run(() => SendMail(recipient_address, mail_title, mail_content )); //fire and forget async ok
        }

         public async Task<string> SendRegistrationConfirmationEmailAsync(string recipient_address, int admin_id, string password, string domain){
            String mail_content = CreateRegistrationMailBody(admin_id, password, domain);
            String mail_title = "Site Leaves Registration confirmation";
            return await SendMail(recipient_address, mail_title, mail_content );
        }

        public void SendFeedbackEmail(string return_email, string feedback){
            String mail_body = CreateFeedbackMailBody(return_email, feedback);
            String mail_title = "Siteleaves Feedback";
            Task.Run(() => SendMail(ConfSettings.Configuration["AppAdminEmail"],mail_title,mail_body));
        }

        public void SendSummaryReportEmail(SummaryReportDto report_data){
            String mail_body = CreateSummaryReportEmailBody( report_data  );
            String mail_title = "Siteleaves Summary Report";
            Task.Run(() => SendMail(ConfSettings.Configuration["AppAdminEmail"],mail_title,mail_body));
        }
    }
}