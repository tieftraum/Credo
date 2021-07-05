using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Credo.Domain.Errors
{
    public class ApiException
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}