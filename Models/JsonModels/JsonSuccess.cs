namespace dynamify.Models.JsonModels
{
    public class JsonSuccess: JsonResponse
    {
        public string response {get;set;}
        public JsonSuccess(string success){
            response = $"{{success: {success}}}";
        }
    }
}