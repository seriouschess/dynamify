using System;
using dynamify.Models;
using dynamify.Models.QueryClasses;
using dynamify.Models.SiteModels;

namespace dynamify.Classes.Auth
{
    public class Auth
    {
        protected AdminQueries dbQueryA;
        private SiteQueries dbQueryS;
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

        public Admin RequestAdmin(string email, string password){ //returns admin with access token
             Admin QueryAdmin = dbQueryA.loginAdmin(email);
            if( password == QueryAdmin.password && email == QueryAdmin.email){
                return QueryAdmin;
            }else{
                Admin ErrorAdmin = new Admin();
                ErrorAdmin.first_name = "<ACCESS DENIED, Password or Email Invalid>";
                ErrorAdmin.last_name = "<ACCESS DENIED, Password or Email Invalid>";
                ErrorAdmin.email = "<ACCESS DENIED, Password or Email Invalid>";
                ErrorAdmin.password = "<ACCESS DENIED, Password or Email Invalid>";
                return ErrorAdmin;
            }
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
            //from base
            dbQueryS = _dbQueryS;
        }

         public bool VerifyAdminForLeaf(int admin_id, int site_id, string token){ //use when modifying a leaf
            System.Console.WriteLine($"Site Id: {site_id}");
            System.Console.WriteLine($"Admin Id: {admin_id}");
            
            if(VerifyAdmin(admin_id, token) == false){
                return false;
            }

            Site QuerySite = new Site();
            QuerySite = dbQueryS.QueryFeaturelessSiteById(site_id);

            if(QuerySite.admin_id == admin_id){
                return true;
            }else{
                return false;
            }
        }
    } 
}