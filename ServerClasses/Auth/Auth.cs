using System.Collections.Generic;
using dynamify.Configuration;
using dynamify.Models;
using dynamify.Models.SiteModels;
using dynamify.ServerClasses.Auth;
using dynamify.ServerClasses.DataLimiter;
using dynamify.ServerClasses.QueryClasses;

namespace dynamify.Classes.Auth
{
    public class Auth
    {
        protected AdminQueries dbQueryA;

        public Auth(AdminQueries _dbQueryA){
            dbQueryA = _dbQueryA;
        }

        public Admin ValidateAdmin(string email, string password){ //returns admin with access token
            List<Admin> QueryAdmins = dbQueryA.GetAdminsByEmail(email);
            
            string err_msg = "Password or Email Invalid";

            int errors = 0;
            Admin QueryAdmin = null;

            if( QueryAdmins.Count ==  0){ //no matching email
                errors += 1;
            }else{
                QueryAdmin = QueryAdmins[0];
                if(QueryAdmin.email_verified == false){
                    errors += 1;
                    err_msg = "Account email has not yet been validated. Check email inbox for activation email.";
                }
                if(!VerifyHash(password, QueryAdmin.password)){
                    errors += 1;
                }
            }

            if(errors > 0){
                throw new System.ArgumentException($"{err_msg}");
            }else{
                return QueryAdmin;
            }
        }

        public string HashString(string unhashed_string){
            int PASSWORD_BCRYPT_COST = 13;
            string PASSWORD_SALT = ConfSettings.Configuration["EncryptionSalt"];
            string salt = "$2a$" + PASSWORD_BCRYPT_COST + "$" + PASSWORD_SALT;
            return BCrypt.Net.BCrypt.HashPassword(unhashed_string, salt);
        }

        public bool VerifyHash( string comparison, string hash ){
            return BCrypt.Net.BCrypt.Verify(comparison, hash);
        }

        public bool VerifyAdmin(int admin_id, string token){ //use to login admin
            Admin QueryAdmin = dbQueryA.GetAdminById(admin_id);
            if(QueryAdmin.token == token && QueryAdmin.email_verified){
                return true;
            }else{
                return false;
            }
        }

         public Admin ReturnValidAdminOrNull(int admin_id, string token){ 
            Admin QueryAdmin = dbQueryA.GetAdminById(admin_id);
            if(QueryAdmin.token == token && QueryAdmin.email_verified){
                return QueryAdmin;
            }else{
                return null;
            }
        }

        public bool VerifyAdminForEmailValidation(int admin_id, string token){ //use to login admin
            try{
                Admin QueryAdmin = dbQueryA.GetAdminById(admin_id);
                if(QueryAdmin.token == token){
                    return true;
                }else{
                    return false;
                }
            }catch{
                return false;
            }
        }

        public bool VerifyAdminByIdToken(int admin_id, string token){ //use to login admin
            try{
                Admin QueryAdmin = dbQueryA.GetAdminById(admin_id);
                if(QueryAdmin.token == token){
                    return true;
                }else{
                    return false;
                }
            }catch{
                return false;
            }
        }
    }

    class SiteAuth: Auth{
        private SiteQueries dbQueryS;
        private DataLimiter _dataLimiter;

        private FieldAuthenticationSuite _fieldValidator = new FieldAuthenticationSuite(); 
        public SiteAuth(AdminQueries _dbQueryA, SiteQueries _dbQueryS, DataLimiter dataLimiter):base(_dbQueryA){
            dbQueryS = _dbQueryS;
            _dataLimiter = dataLimiter;
        }

        public bool VerifyAdminForLeaf(int admin_id, int site_id, string token){ //use when modifying a leaf

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

        public List<string> ValidateIncomingComponent(SiteComponent sc){
            List<string> errors = sc.GetFieldErrors(_fieldValidator);
            return errors;
        }

        public List<string> ValidateIncomingSite(Site site){
            List<string> errors = new List<string>();
            if(!_fieldValidator.ValidateTitleLength(site.title)){
                errors.Add(_fieldValidator.TitleFieldTooLongMessage("Site Title", site.title.Length));
            }
            return errors;
        }
    } 
}