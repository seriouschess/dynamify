using System.Collections.Generic;

namespace dynamify.Models.SiteModels
{
    public partial class ParagraphBox
    {
        public override int FindCharLength(){
            int character_sum = 0;
            character_sum += this.title.Length;
            character_sum += this.content.Length;
            return character_sum;
        }
        
        public override List<string> GetFieldErrors(ServerClasses.Auth.FieldAuthenticationSuite s){
            List<string> errors = new List<string>();
            if(!s.ValidateTitleLength(this.title)){
                errors.Add(s.TitleFieldTooLongMessage("Title", this.title.Length));
            }
            
            if(!s.ValidateParagraphContentLength(this.content)){
                errors.Add(s.ContentFieldTooLongMessage("Content Field", this.content.Length));
            }
            return errors;
        }
    }
}