namespace dynamify.ServerClasses.Auth
{
    public class FieldAuthenticationSuite
    {
        private int _paragraph_content_max_length = 10000;
        private int _title_max_length = 50;

        private int _image_base_sixty_four_max = 753483; //half a meg and change
        
        //validators
        public bool ValidateTitleLength(string str){
            if(str.Length > this._title_max_length){
                return false;
            }else{
                return true;
            }
        }

        public bool ValidateParagraphContentLength(string str){
            if( str.Length > this._paragraph_content_max_length){
                return false;
            }else{
                return true;
            }
        }

        public bool ValidateImageBase64Length(string str){
            if(str.Length > 753483){
                return false;
            }
            return true;
        }

        public bool ValidateImageBase64FileType(string str){
            string test_string = str.Substring(0, 100);
            if(!test_string.Contains("png") && !test_string.Contains("jpg")){
                return false;
            }
            return true;
        }

        //generic messages
        public string TitleFieldTooLongMessage(string title_name, int string_length){
            if(string_length < this._title_max_length){
                throw new System.ArgumentException("Title validation error.");
            }
            return $"The {title_name} has a length of {string_length} characters which is over the limit of {this._title_max_length}.";
        }

        public string ContentFieldTooLongMessage(string content_name, int string_length){
            if(string_length < this._paragraph_content_max_length){
                throw new System.ArgumentException("Content validation error.");
            }
            return $"The {content_name} has a length of {string_length} characters which is over the limit of {this._paragraph_content_max_length}.";
        }

        public string ImageBase64FileTooLargeMessage(string content_name, int string_length){
            if(string_length < this._image_base_sixty_four_max){
                throw new System.ArgumentException("Image B64 validation error.");
            }
            return $"The {content_name} has a file size of {string_length} bytes which exceeds the limit of 0.5 MB.";
        }

        public string ImageInvalidFileTypeMessage(string content_name){
            return $"The {content_name} is neither a .png nor a .jpg";
        }
    }
}