using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace dynamify.Models
{
    public class RawJsonBodyInputFormatter : InputFormatter
    {
         //allows the importing of json as a string into ASP.NET
        public RawJsonBodyInputFormatter()
        {
            this.SupportedMediaTypes.Add("application/json");
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            using (var reader = new StreamReader(request.Body))
            {
                var content = await reader.ReadToEndAsync();
                return await InputFormatterResult.SuccessAsync(content);
            }
        }
        
        protected override bool CanReadType(Type type)
        {
            return type == typeof(string);
        }
    }
}