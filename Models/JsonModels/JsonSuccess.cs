namespace dynamify.Models.JsonModels
{
    public class JsonSuccess: JsonResponse
    {
        public JsonSuccess(string success){
            response = $"{{success: {success}}}";
        }
    }
}