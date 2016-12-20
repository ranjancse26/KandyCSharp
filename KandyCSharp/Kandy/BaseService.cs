using Newtonsoft.Json;
using RestSharp;

namespace KandyCSharp.Kandy
{
    public class BaseService
    {
        public string ExtractMessageFromResponse(IRestResponse response)
        {
            if (response == null)
                return string.Empty;

            var responseObject = (dynamic)JsonConvert.DeserializeObject(response.Content);
            if (responseObject != null)
                return responseObject.message;

            return string.Empty;
        }
    }
}
