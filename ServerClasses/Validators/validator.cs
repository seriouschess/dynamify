using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using dynamify.Models;
using dynamify.Models.QueryClasses;

namespace dynamify.ServerClasses.Validators
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

            System.Console.WriteLine($"Email bogie: {email_bogie}");
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

        public string ValidateAdmin(Admin NewAdmin){
            int errors = 0;
            System.Console.WriteLine("doe");

            if( NewAdmin.first_name == "" ){
                errors += 1;
                System.Console.WriteLine("ray");
            }

            if( NewAdmin.last_name == "" ){
                errors += 1;
                System.Console.WriteLine("me");
            }

            if( !ValidateEmail(NewAdmin.email) ){
                errors += 1;
                System.Console.WriteLine("fa");
            }

            if(NewAdmin.password.Length < 8){
                errors += 1;
                System.Console.WriteLine("so");
            }

            if(errors == 0){
                System.Console.WriteLine("la");
                if(CheckUniqueEmail(NewAdmin.email)){
                    return "pass";
                }else{
                    return "duplicate email";
                }
            }else{
                System.Console.WriteLine("ti");
                return "invalid credentials";
            }

        }
    }
}