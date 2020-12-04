using System.Collections.Generic;

namespace dynamify.Models.SiteModels
{
    public partial class NavLink
    {
        public int FindCharLength(){
            int character_sum = 0;
            character_sum += this.label.Length;
            character_sum += this.url.Length;
            return character_sum;
        }
      public List<string> GetFieldErrors(ServerClasses.Auth.FieldAuthenticationSuite s){
            List<string> errors = new List<string>();

            if(!s.ValidateParagraphContentLength(this.label)){
                errors.Add(s.ContentFieldTooLongMessage("Link URL", this.label.Length));
            }

            if(!s.ValidateParagraphContentLength(this.url)){
                errors.Add(s.ContentFieldTooLongMessage("Link Box Content", this.url.Length));
            }
            return errors;
        }
    }
}