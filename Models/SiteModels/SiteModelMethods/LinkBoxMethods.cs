using System.Collections.Generic;

namespace dynamify.Models.SiteModels
{
    public partial class LinkBox
    {
        public override int FindCharLength(){
            int character_sum = 0;
            character_sum += this.content.Length;
            character_sum += this.url.Length;
            character_sum += this.link_display.Length;
            character_sum += this.title.Length;
            return character_sum;
        }
      public override List<string> GetFieldErrors(ServerClasses.Auth.FieldAuthenticationSuite s){
            List<string> errors = new List<string>();
            if(!s.ValidateTitleLength(this.title)){
                errors.Add(s.TitleFieldTooLongMessage("Title", this.title.Length));
            }
            
            if(!s.ValidateTitleLength(this.link_display)){
                errors.Add(s.TitleFieldTooLongMessage("Link Display Text", this.link_display.Length));
            }

            if(!s.ValidateParagraphContentLength(this.url)){
                errors.Add(s.ContentFieldTooLongMessage("Link URL", this.url.Length));
            }

            if(!s.ValidateParagraphContentLength(this.content)){
                errors.Add(s.ContentFieldTooLongMessage("Link Box Content", this.content.Length));
            }
            return errors;
        }
    }

}