using System;
using System.Collections.Generic;
using dynamify.Models;
using dynamify.Models.QueryClasses;
using dynamify.Models.SiteModels;

namespace dynamify.Classes.Auth
{
    public class Auth
    {
        protected AdminQueries dbQueryA;
        private string[] charset = {
            "1","2","3","4","5","6","7","8","9","0",
            "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
            "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
            };
        
        public Auth(AdminQueries _dbQueryA){
            dbQueryA = _dbQueryA;
        }

        public Token Generate(){ //produces a random string of length 15 using charset
            Random rand = new Random();
            string auth_token = "";
            for(int x=0; x<15 ;x++){
                auth_token += charset[rand.Next(0, charset.Length)];
            }
            return new Token(auth_token);
        }

        public Admin ValidateAdmin(string email, string password){ //returns admin with access token
            List<Admin> QueryAdmins = dbQueryA.GetAdminsByEmail(email);

            Admin ErrorAdmin = new Admin();

            string err_msg = "<ACCESS DENIED, Password or Email Invalid>";
            ErrorAdmin.first_name = err_msg;
            ErrorAdmin.last_name = err_msg;
            ErrorAdmin.email = err_msg;
            ErrorAdmin.password = err_msg;

            int errors = 0;
            Admin QueryAdmin = null;

            if( QueryAdmins.Count ==  0){ //no matching email
                errors += 1;
                System.Console.WriteLine("Email Denied");
            }else{
                QueryAdmin = QueryAdmins[0];
                if(!VerifyHash(password, QueryAdmin.password)){
                    System.Console.WriteLine($"Password: {password}");
                    System.Console.WriteLine($"Hashed Password: {QueryAdmin.password}");
                    System.Console.WriteLine("Password Denied");
                    errors += 1;
                }
            }

            if(errors > 0){
                return ErrorAdmin;
            }else{
                System.Console.WriteLine("Password Accepted");
                return QueryAdmin;
            }
        }

        public string HashString(string unhashed_string){
            int PASSWORD_BCRYPT_COST = 13;
            string PASSWORD_SALT = "/8Wncr26eAmxD1l6cAF9F8";
            string salt = "$2a$" + PASSWORD_BCRYPT_COST + "$" + PASSWORD_SALT;
            return BCrypt.Net.BCrypt.HashPassword(unhashed_string, salt);
        }

        public bool VerifyHash( string comparison, string hash ){
            //string hashed_comparison = HashString(comparison);
            return BCrypt.Net.BCrypt.Verify(comparison, hash);
        }

        public bool VerifyAdmin(int admin_id, string token){ //use to login admin
            Admin QueryAdmin = dbQueryA.GetAdminById(admin_id);
            if(QueryAdmin.token == token){
                return true;
            }else{
                return false;
            }
        }
    }

    class SiteAuth: Auth{
        private SiteQueries dbQueryS;
        public SiteAuth(AdminQueries _dbQueryA, SiteQueries _dbQueryS):base(_dbQueryA){
            dbQueryS = _dbQueryS;
        }

         public bool VerifyAdminForLeaf(int admin_id, int site_id, string token){ //use when modifying a leaf
            System.Console.WriteLine($"Site Id: {site_id}");
            System.Console.WriteLine($"Admin Id: {admin_id}");

            if(VerifyAdmin(admin_id, token) == false){
                return false;
            }

            Site QuerySite = dbQueryS.QueryFeaturelessSiteById(site_id);

            if(QuerySite.admin_id == admin_id){
                return true;
            }else{
                return false;
            }
        }
    } 
}