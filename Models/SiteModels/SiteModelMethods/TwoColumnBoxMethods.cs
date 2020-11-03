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
    }
}