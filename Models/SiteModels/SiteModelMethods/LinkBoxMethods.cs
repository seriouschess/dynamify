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
    }
}