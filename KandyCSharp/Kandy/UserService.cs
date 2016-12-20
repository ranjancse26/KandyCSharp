using System.Net;
using KandyCSharp.Entities;
using Newtonsoft.Json;
using RestSharp;

namespace KandyCSharp.Kandy
{
    public class UserService
    {
        private readonly string _baseUrl;
        private readonly string _accessToken;

        public UserService(string accessToken)
        {
            _accessToken = accessToken;
            _baseUrl = Configuration.GetKandyBaseUrl();
        }

        /// <summary>
        /// Fetch devices
        /// </summary>
        /// <param name="accessToken">Access Token</param>
        /// <returns>DeviceResultCreate</returns>
        public DeviceResultCreate GetDevices(string accessToken)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(_baseUrl);
            var url = "users/devices?key=" + accessToken;
            var request = new RestRequest(url, Method.GET);
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<DeviceResultCreate>(response.Content);
        }
    }
}
