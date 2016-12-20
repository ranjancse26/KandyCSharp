using System;
using System.Net;
using KandyCSharp.Entities;
using Newtonsoft.Json;
using RestSharp;

namespace KandyCSharp.Kandy
{
    public class DomainService
    {
        private readonly string _baseUrl = Configuration.GetKandyBaseUrl();
        private readonly string _domainApiKey = Configuration.GetDomainAPIKey();
        private readonly string _domainSecretKey = Configuration.GetDomainSecretKey();

        /// <summary>
        /// Get DomainService Access Token
        /// </summary>
        /// <returns>Access Token</returns>
        public string GetDomainAccessToken()
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(_baseUrl);
            var url = "domains/accesstokens?key=" + _domainApiKey 
                + "&domain_api_secret=" + _domainSecretKey;
            var request = new RestRequest(url, Method.GET);

            // execute the request
            IRestResponse response = client.Execute(request);

            var domainAccess =
                JsonConvert.DeserializeObject<DomainAccessEntity>(response.Content);

            return domainAccess.DomainAccess.AccessToken;
        }
        
        /// <summary>
        /// Delete DomainService Access Token
        /// </summary>
        /// <param name="accessToken">Access Token</param>
        /// <returns>Success or not</returns>
        public string DeleteDomainAccessToken(string accessToken)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(_baseUrl);
            var url = "domains/accesstokens?key=" + _domainApiKey +
                      "&domain_api_secret=" + _domainSecretKey +
                      "&domain_access_token=" + accessToken;

            var request = new RestRequest(url, Method.DELETE);
            IRestResponse response = client.Execute(request);
            var responseObject =
                (dynamic)JsonConvert.DeserializeObject(response.Content);

            if (responseObject != null)
                return responseObject.message;

            return string.Empty;
        }

        /// <summary>
        /// Add user by Phone Number
        /// </summary>
        /// <param name="userEntity">User Entity</param>
        /// <returns>Success or Failure</returns>
        public string CreateUserByPhoneNumber(UserEntity userEntity, string domainAccessToken)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(_baseUrl);
            var url = "domains/users/phone_number?key=" + domainAccessToken;
            var request = new RestRequest(url, Method.POST);

            if (string.IsNullOrEmpty(userEntity.PhoneNumber))
                throw new ApplicationException("User Phone Number is Required");

            request.AddHeader("content-type", "application/json");

            request.AddParameter("user_phone_number", 
                userEntity.PhoneNumber, ParameterType.RequestBody);

            if (!string.IsNullOrEmpty(userEntity.CountryCode))
                request.AddParameter("user_country_code",
                    userEntity.CountryCode, ParameterType.RequestBody);

            if (!string.IsNullOrEmpty(userEntity.FirstName))
                request.AddParameter("user_first_name", 
                    userEntity.FirstName, ParameterType.RequestBody);

            if (!string.IsNullOrEmpty(userEntity.LastName))
                request.AddParameter("user_last_name",
                    userEntity.LastName, ParameterType.RequestBody);

            if (!string.IsNullOrEmpty(userEntity.Email))
                request.AddParameter("user_email", 
                    userEntity.Email, ParameterType.RequestBody);

            if (!string.IsNullOrEmpty(userEntity.Password))
                request.AddParameter("user_password", 
                    userEntity.Password, ParameterType.RequestBody);

            // execute the request
            IRestResponse response = client.Execute(request);
            var responseObject = (dynamic)JsonConvert.DeserializeObject(response.Content);

            if (responseObject != null)
                return responseObject.message;

            return string.Empty;
        }
    }
}
