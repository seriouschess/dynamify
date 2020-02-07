namespace dynamify.Models.JsonModels
{
    public class JsonFailure : JsonResponse
    {
        public JsonFailure(string failure){
            response = $"{{failure: {failure}}}";
        }
    }
}