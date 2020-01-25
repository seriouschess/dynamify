namespace dynamify.Models.JsonModels
{
    public class JsonFailure : JsonResponse
    {
        public string response {get;set;}
        public JsonFailure(string failure){
            response = $"{{failure: {failure}}}";
        }
    }
}