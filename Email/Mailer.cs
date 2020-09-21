using System.Net.Mail;
using System.Net;
using System;
using System.Threading.Tasks;

using dynamify.Configuration;

namespace dynamify.Email
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

        public String CreateRegistrationMailBody(string email, string password){
            String new_body = "<h1>Thank you for registering to siteleaves.com</h1>" +
            "<p>You may now log in using the following information:</p>" +
            $"<p>Email: {email}</p>" +
            $"<p>Password: {password}</p><br>"+
            $"<p>Get started at: <a href='https://siteleaves.com'>siteleaves.com</a></p>";
            return new_body; 
        }

        public String CreatePasswordResetMailBody(string email, string token){
            String new_body = "<h2>Site Leaves account password reset</h2>" +
            "<p>You've requested to reset your account password. We've got you!</p>" +
            "<p>Click the following link to reset your password:</p>" +
            $"<p><a href='https://siteleaves.com'>Click Here</a></p>";
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

        public async Task<string> SendPasswordResetMail(string user_email, string new_token){
            String mail_content = CreatePasswordResetMailBody(user_email, new_token);
            String mail_title = "Site Leaves account password reset";
            return await SendMail(user_email, mail_title, mail_content);
        }

        public void SendRegistrationConfirmationEmail(string recipient_address, string password){
            String mail_content = CreateRegistrationMailBody(recipient_address, password);
            String mail_title = "Site Leaves Registration confirmation";
            Task.Run(() => SendMail(recipient_address, mail_title, mail_content )); //fire and forget async ok
        }

         public async Task<string> SendRegistrationConfirmationEmailAsync(string recipient_address, string password){
            String mail_content = CreateRegistrationMailBody(recipient_address, password);
            String mail_title = "Site Leaves Registration confirmation";
            return await SendMail(recipient_address, mail_title, mail_content );
        }
    }
}