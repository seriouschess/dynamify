using System.Collections.Generic;

namespace dynamify.Models.SiteModels
{
    public partial class TwoColumnBox
    {
        public override int FindCharLength(){
            int character_sum = 0;
            character_sum += this.title.Length;
            character_sum += this.content_one.Length;
            character_sum += this.content_two.Length;
            character_sum += this.heading_one.Length;
            character_sum += this.heading_two.Length;
            return character_sum;
        }

        public override List<string> GetFieldErrors(ServerClasses.Auth.FieldAuthenticationSuite s){
            List<string> errors = new List<string>();
            if(!s.ValidateTitleLength(this.title)){
                errors.Add(s.TitleFieldTooLongMessage("Title", this.title.Length));
            }
            
            if(!s.ValidateTitleLength(this.heading_one)){
                errors.Add(s.TitleFieldTooLongMessage("Left Heading", this.heading_one.Length));
            }
            if(!s.ValidateTitleLength(this.heading_two)){
                errors.Add(s.TitleFieldTooLongMessage("Right Heading", this.heading_two.Length));
            }
            if(!s.ValidateParagraphContentLength(this.content_one)){
                errors.Add(s.ContentFieldTooLongMessage("Left Content Field", this.content_one.Length));
            }
            if(!s.ValidateParagraphContentLength(this.content_two)){
                errors.Add(s.ContentFieldTooLongMessage("Right Content Field", this.content_two.Length));
            }
            return errors;
        }
    }
}