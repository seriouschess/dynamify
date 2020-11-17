namespace dynamify.ServerClasses.Analytics
{
    public class Formatter
    {
        public Formatter(){}

        public string StripDomainFromUrl(string unformatted_url){
            string output_url = "";
            for(int x=0; x<unformatted_url.Length; x++){
                char c = unformatted_url[x];
                if( c == '/'  ){
                    output_url = "";
                }else{
                    output_url += c;
                }
            }
            return output_url;
        }
    }
}