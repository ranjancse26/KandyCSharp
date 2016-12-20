using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace KandyCSharp.Kandy
{
    public class UserAccessTokenService
    {
        private readonly string baseUrl = Configuration.GetKandyBaseUrl();
        private readonly string _domainApiKey = Configuration.GetDomainAPIKey();
        private readonly string _domainSecretKey = Configuration.GetDomainSecretKey();
      
        public string GetUserAccessToken(string userId)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(baseUrl);
            var url = "domains/users/accesstokens?key=" + _domainApiKey +
                      "&domain_api_secret=" + _domainSecretKey +
                      "&user_id=" + userId;

            var request = new RestRequest(url, Method.GET);
            IRestResponse response = client.Execute(request);
            var responseObject = (dynamic)JsonConvert.DeserializeObject(response.Content);

            if (responseObject != null && responseObject.result != null)
                return responseObject.result.user_access_token;

            return string.Empty;
        }

        public string DeleteUserAccessToken(string userId, string password, string accessToken)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(baseUrl);
            var url = "domains/users/accesstokens?key=" + _domainApiKey +
                      "&user_id=" + userId +
                      "&user_password=" + password +
                      "&user_access_token=" + accessToken;

            var request = new RestRequest(url, Method.DELETE);
            IRestResponse response = client.Execute(request);
            var responseObject = (dynamic)JsonConvert.DeserializeObject(response.Content);

            if (responseObject != null)
                return responseObject.message;

            return string.Empty;
        }
    }
}
