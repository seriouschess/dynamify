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
    }
}