using System;

namespace dynamify.ServerClasses.Auth
{
    public class TokenGenerator
    {
        private string[] charset = {
            "1","2","3","4","5","6","7","8","9","0",
            "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
            "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
            };

        public TokenGenerator(){ }

        public string GenerateToken(){ //produces a random string of length 45 using charset
            Random rand = new Random();
            string auth_token = "";
            for(int x=0; x<45 ;x++){
                auth_token += charset[rand.Next(0, charset.Length)];
            }
            return auth_token;
        }
    }
}