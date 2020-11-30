using System.Collections.Generic;

namespace dynamify.Models.SiteModels
{
    public abstract partial class SiteComponent
    {
        //when called, returns the number of characters used by the component
        public abstract int FindCharLength();
        public abstract List<string> GetFieldErrors(ServerClasses.Auth.FieldAuthenticationSuite e);
    }
}