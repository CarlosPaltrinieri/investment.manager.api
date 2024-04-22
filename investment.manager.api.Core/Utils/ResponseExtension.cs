using Newtonsoft.Json;
using System.Net;

namespace investiment.manager.api.Utils
{
    public class ResponseExtension
    {
        public int StatusCode { get; set; }
        public string Body { get; set; }
        public ResponseExtension Response(HttpStatusCode statusCode, object data)
        {

            return new ResponseExtension
            {
                StatusCode = (int)statusCode,
                Body = JsonConvert.SerializeObject(data, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore })
            };

        }
    }
}
