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
    }
}