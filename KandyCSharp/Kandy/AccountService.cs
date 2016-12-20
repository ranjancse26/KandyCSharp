using System;
using System.Net;
using KandyCSharp.Entities;
using Newtonsoft.Json;
using RestSharp;

namespace KandyCSharp.Kandy
{
    public class AccountService
    {
        private readonly string baseUrl = Configuration.GetKandyBaseUrl();
        private readonly string _accountApiKey = Configuration.GetApplicationAPIKey();
        private readonly string _accountSecretKey = Configuration.GetApplicationSecretKey();

        /// <summary>
        /// Get Account Access Token
        /// </summary>
        /// <returns>Access Token</returns>
        public string GetAccountAccessToken()
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(baseUrl);
            var url = "accounts/accesstokens?key=" + _accountApiKey
                + "&account_api_secret=" + _accountSecretKey;
            var request = new RestRequest(url, Method.GET);

            // execute the request
            IRestResponse response = client.Execute(request);
            var responseObject =
              (dynamic)JsonConvert.DeserializeObject(response.Content);

            if (responseObject != null && responseObject.result != null)
            {
                if (responseObject.message.ToString().Equals("success"))
                    return responseObject.result.account_access_token;
            }

            return string.Empty;
        }

        /// <summary>
        /// Fetch all User Domains
        /// </summary>
        /// <param name="accessToken">access token</param>
        /// <returns>DomainResultEntity</returns>
        public DomainResultEntity GetDomains(string accessToken)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(baseUrl);
            var url = "accounts/domains?key=" + accessToken;
            var request = new RestRequest(url, Method.GET);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<DomainResultEntity>
                     (response.Content);
            }

            return null;
        }

        /// <summary>
        /// Create Domain by Domain and Project Name
        /// </summary>
        /// <returns>Return the created domain name or error</returns>
        public string CreateDomain(string accessToken, string domainName,
                                   string projectName = "")
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(baseUrl);
            var url = "accounts/domains?key=" + accessToken;

            if (string.IsNullOrEmpty(domainName))
                throw new ApplicationException("Domain name is Required");

            var request = new RestRequest(url, Method.POST);

            request.AddParameter("domain_name", domainName.ToLower());

            if (!string.IsNullOrEmpty(projectName))
                request.AddParameter("project_name", projectName);

            IRestResponse response = client.Execute(request);
            var responseObject =
                (dynamic)JsonConvert.DeserializeObject(response.Content);

            if (responseObject != null)
            {
                if (responseObject.message != null)
                    return responseObject.message;

                return responseObject.domain_name;
            }

            return string.Empty;
        }

        /// <summary>
        /// Delete Account Access Token
        /// </summary>
        /// <param name="accessToken">Access Token</param>
        /// <returns>Success or not</returns>
        public bool DeleteAccountAccessToken(string accessToken)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(baseUrl);
            var url = "accounts/accesstokens?key=" + _accountApiKey +
                      "&account_api_secret=" + _accountSecretKey +
                      "&account_access_token=" + accessToken;

            var request = new RestRequest(url, Method.DELETE);
            IRestResponse response = client.Execute(request);
            var responseObject =
                (dynamic)JsonConvert.DeserializeObject(response.Content);

            if (responseObject != null)
                return responseObject.message.Equals("success");

            return false;
        }

        /// <summary>
        /// Delete Domain
        /// </summary>
        /// <param name="accessToken">Access Token</param>
        /// <param name="domainApiKey">API Key of Domain to Delete</param>
        /// <returns>Success or not</returns>
        public bool DeleteDomain(string accessToken, string domainApiKey)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(baseUrl);
            var url = "accounts/domains?key=" + accessToken +
                      "&domain_api_key=" + domainApiKey;

            var request = new RestRequest(url, Method.DELETE);
            IRestResponse response = client.Execute(request);
            var responseObject =
                (dynamic)JsonConvert.DeserializeObject(response.Content);

            if (responseObject != null)
                return responseObject.message.Equals("Domain Deleted");

            return false;
        }
    }
}
