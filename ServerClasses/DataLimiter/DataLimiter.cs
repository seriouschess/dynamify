using dynamify.Models.SiteModels;

namespace dynamify.ServerClasses.DataLimiter
{
    public class DataLimiter
    {
        public DataLimiter(){

        }

        public bool ValidateParagraphBox( ParagraphBox unknown, int max_data){
            if( FindParagraphBoxCharLength( unknown ) > max_data ){
                return false;
            }else{
                return true;
            }
        }

        //this method makes estimates
        public int ConvertCharLengthToBytes(int character_sum){
            return character_sum;
        }

        public int FindParagraphBoxCharLength(ParagraphBox unknown){
            int character_sum = 0;
            character_sum += unknown.title.Length;
            character_sum += unknown.content.Length;
            return character_sum;
        }

        public int FindTwoColumnBoxCharLength(TwoColumnBox unknown){
            int character_sum = 0;
            character_sum += unknown.title.Length;
            character_sum += unknown.content_one.Length;
            character_sum += unknown.content_two.Length;
            character_sum += unknown.heading_one.Length;
            character_sum += unknown.heading_two.Length;
            return character_sum;
        }

        public int FindImageCharLength( Image unknown ){
            int character_sum = 0;
            character_sum += unknown.title.Length;
            character_sum += unknown.image_src.Length;
            return character_sum;
        }

        public int FindLinkBoxCharLength( LinkBox unknown){
            int character_sum = 0;
            character_sum += unknown.content.Length;
            character_sum += unknown.url.Length;
            character_sum += unknown.link_display.Length;
            character_sum += unknown.title.Length;
            return character_sum;
        }

        public int FindPortraitCharLength( Portrait unknown){
            int character_sum = 0;
            character_sum += unknown.title.Length;
            character_sum += unknown.image_src.Length;
            character_sum += unknown.content.Length;
            return character_sum;
        }
    }
}