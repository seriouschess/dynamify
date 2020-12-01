namespace dynamify.ServerClasses.Auth
{
    public class FieldAuthenticationSuite
    {
        private int _paragraph_content_max_length = 20000;
        private int _title_max_length = 200;

        private int _image_base_sixty_four_max = 2000000; //two megabytes
        
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
            if(str.Length > _image_base_sixty_four_max){
                return false;
            }
            return true;
        }

        public bool ValidateImageBase64FileType(string str){
            System.Console.WriteLine(str);

            int test_end;
            if(str.Length >= 100){
                test_end = 100;
            }else{
                test_end = str.Length;
            }
            string test_string = str.Substring(0, test_end);

            System.Console.WriteLine($"TEST STRING: {test_string} Contains PNG: {test_string.Contains("png").ToString()} Contains JPG: {test_string.Contains("jpg").ToString()}");
            if(!test_string.Contains("png") && !test_string.Contains("jpg") && !test_string.Contains("jpeg")){
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
            return $"The {content_name} has a file size of {string_length} bytes which exceeds the limit of 2 MB.";
        }

        public string ImageInvalidFileTypeMessage(string content_name){
            return $"The {content_name} is neither a .png nor a .jpg";
        }
    }
}