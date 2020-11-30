using System.Collections.Generic;

namespace dynamify.Models.SiteModels
{
    public partial class Image
    {
        public override int FindCharLength(){
            int character_sum = 0;
            character_sum += this.title.Length;
            character_sum += this.image_src.Length;
            return character_sum;
        }

        public override List<string> GetFieldErrors(ServerClasses.Auth.FieldAuthenticationSuite s){
            List<string> errors = new List<string>();
            
            if(s.ValidateTitleLength(this.title)){
                errors.Add(s.TitleFieldTooLongMessage("Title", this.title.Length));
            }

            if(s.ValidateImageBase64Length(this.image_src)){
                errors.Add(s.ImageBase64FileTooLargeMessage("Image", this.image_src.Length));
            }

            if(s.ValidateImageBase64FileType(this.image_src)){
                errors.Add(s.ImageInvalidFileTypeMessage("Image"));
            }

            return errors;
        }
    }
}