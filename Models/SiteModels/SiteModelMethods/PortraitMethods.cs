using System.Collections.Generic;

namespace dynamify.Models.SiteModels
{
    public partial class Portrait
    {
        public override int FindCharLength(){
            int character_sum = 0;
            character_sum += this.title.Length;
            character_sum += this.image_src.Length;
            character_sum += this.content.Length;
            return character_sum;
        }

        public override List<string> GetFieldErrors(ServerClasses.Auth.FieldAuthenticationSuite s){
            List<string> errors = new List<string>();
            
            if(!s.ValidateTitleLength(this.title)){
                errors.Add(s.TitleFieldTooLongMessage("Title", this.title.Length));
            }

            if(!s.ValidateImageBase64Length(this.image_src)){
                errors.Add(s.ImageBase64FileTooLargeMessage("Portrait Image", this.image_src.Length));
            }

            if(!s.ValidateImageBase64FileType(this.image_src)){
                errors.Add(s.ImageInvalidFileTypeMessage("Portrait Image"));
            }

            if(!s.ValidateParagraphContentLength(this.content)){
                errors.Add(s.ContentFieldTooLongMessage("Portrait Content", this.content.Length));
            }

            return errors;
        }
    }
}