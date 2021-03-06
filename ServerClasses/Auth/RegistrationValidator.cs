using System.Collections.Generic;
using System.Text.RegularExpressions;
using dynamify.Models;
using dynamify.ServerClasses.QueryClasses;
using dynamify.Models.SiteModels;

namespace dynamify.ServerClasses.Auth
{
    public class RegistrationValidator
    {
        private AdminQueries dbQuery;
        public RegistrationValidator(AdminQueries _dbQuery){
            dbQuery = _dbQuery;
        }

        public bool ValidateEmail(string email_bogie){
            //two checks because not all email validators are perfect. A more ideal system may be implemented
            int fails = 0;

            //check Microsoft MailAddress format
            // try
            // {
            //     MailAddress m = new MailAddress(email_bogie);
            //     //do nothing, success
            // }
            // catch (FormatException) //not valid email format
            // {
            //     fails += 1;
            // }

            //check Valid email Regex -- Removed to avoid inconsistencies with frontend check
            Regex regex = new Regex(
                @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"+ "@"
                + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$"
            );
            Match match = regex.Match(email_bogie);
            if (match.Success){
                //do nothing, success
            }else{
                fails += 1;
            }

            if(fails == 0){
                return true;
            }else{
                return false;
            }      
        }

        public bool CheckUniqueEmail(string email_bogie){
             List<Admin> QueryAdmins = dbQuery.GetAdminsByEmail(email_bogie);
            if(QueryAdmins.Count == 0){
                return true;
            }else{
                return false; //duplicate exists
            }
        }

        public bool CheckUniqueUsername(string username_bogie){
            List<Admin> QueriedAdmins = dbQuery.GetAdminsByUsername(username_bogie);
            if(QueriedAdmins.Count == 0){
                return true;
            }else{
                return false;
            }
        }

        public string ValidateAdmin(Admin NewAdmin){
            int errors = 0;
            string invalid_message = "";

            if( NewAdmin.username == "" ){
                errors += 1;
                invalid_message = "Please add a username to identify the account.";
            }

            if(!CheckUniqueUsername(NewAdmin.username)){
                errors += 1;
                invalid_message = "That user name already exists. Try adding a different user name.";
            }

            if(!CheckUniqueEmail(NewAdmin.email) ){
                errors += 1;
                invalid_message = "That email is being used by another account. Try contacting support or using a different email.";
            }

            if(NewAdmin.password.Length < 8){
                errors += 1;
                invalid_message = "Password must be 8 characters long.";
            }

            if(errors == 0){
                return "pass";
            }else{
                return invalid_message;
            }

        }
    }

    public class SiteCreationValidator {

        private SiteQueries SiteDbContext;
        public SiteCreationValidator(SiteQueries _SitedBContext){ 
            SiteDbContext = _SitedBContext;
        }

        public string ValidateSiteUrl(string leaf_url){
            List<Site> test = SiteDbContext.QueryFeaturelessSiteByUrl(leaf_url);
            
            if(test.Count > 0 || leaf_url == "app" || leaf_url == "default" || leaf_url == "api" || leaf_url == "swagger" ){
                return "Site must not have duplicate title with existing site.";
            }

            Regex regex = new Regex( @"^[a-zA-ZäöüßÄÖÜ]+$" );

            Match match = regex.Match(leaf_url);

            if (match.Success){
                //do nothing, success
            }else{
                return "Incomprehensible leaf url. Avoid using special characters.";
            }

            return "pass";
        }
    }
}