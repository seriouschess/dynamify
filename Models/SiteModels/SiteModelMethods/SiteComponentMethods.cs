namespace dynamify.Models.SiteModels
{
    public abstract partial class SiteComponent
    {
        //when called, returns the number of characters used by the component
        public abstract int FindCharLength();
    }
}