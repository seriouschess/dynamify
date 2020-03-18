using System;
using dynamify.Models;
using dynamify.Models.QueryClasses;

namespace dynamify.Classes.Auth
{
    public class Auth
    {
        private AdminQueries dbQuery;
        private string[] charset = {
            "1","2","3","4","5","6","7","8","9","0",
            "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
            "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
            };
        
        public Auth(AdminQueries _dbQuery){
            dbQuery = _dbQuery;
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
             Admin QueryAdmin = dbQuery.loginAdmin(email);
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

        public bool VerifyAdmin(int admin_id, string token){
            Admin QueryAdmin = dbQuery.GetAdminById(admin_id);
            if(QueryAdmin.token == token){
                return true;
            }else{
                return false;
            }
        }
    } 
}