using System.Net;
using KandyCSharp.Entities;
using Newtonsoft.Json;
using RestSharp;

namespace KandyCSharp.Kandy
{
    public class UserAddressBookService
    {
        private readonly string _baseUrl;
        private readonly string _accessToken;

        public UserAddressBookService(string accessToken)
        {
            _accessToken = accessToken;
            _baseUrl = Configuration.GetKandyBaseUrl();
        }
     
        /// <summary>
        /// Fetch all user contacts
        /// </summary>
        /// <returns></returns>
        public ContactResultRootEntity GetContacts()
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(_baseUrl);
            var url = "users/addressbooks/personal?key=" + _accessToken;

            var request = new RestRequest(url, Method.GET);
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<ContactResultRootEntity>(response.Content);
        }

        /// <summary>
        /// Add user contact
        /// </summary>
        /// <param name="contactEntity">Contact Entity</param>
        /// <returns>Contact Id</returns>
        public string AddContact(ContactEntity contactEntity)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var url = _baseUrl + "users/addressbooks/personal?key=" + _accessToken;
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json",
              " {\"contact\":" +
              JsonConvert.SerializeObject(contactEntity)
              + "\n}",
               ParameterType.RequestBody);

            // execute the request
            IRestResponse response = client.Execute(request);
            var responseObject = (dynamic)JsonConvert.DeserializeObject(response.Content);

            if (responseObject != null && responseObject.result != null)
                return responseObject.result.contact_id;

            return string.Empty;
        }

        /// <summary>
        /// Delete User Contact
        /// </summary>
        /// <param name="contactId">Contact Id</param>
        /// <returns>success or not</returns>
        public string DeleteContact(string contactId)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(_baseUrl);
            var url = "users/addressbooks/personal?key=" + _accessToken +
                      "&contact_id=" + contactId;
            var request = new RestRequest(url, Method.POST);

            IRestResponse response = client.Execute(request);
            var responseObject = (dynamic)JsonConvert.DeserializeObject(response.Content);

            if (responseObject != null)
                return responseObject.message;

            return string.Empty;
        }
    }
}
