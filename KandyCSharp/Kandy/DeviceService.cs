using System;
using System.Collections.Generic;
using System.Net;
using KandyCSharp.Entities;
using KandyCSharp.Entities.Message;
using Newtonsoft.Json;
using RestSharp;

namespace KandyCSharp.Kandy
{
    public class DeviceService : BaseService
    {
        private readonly string _baseUrl = Configuration.GetKandyBaseUrl();

        /// <summary>
        /// Create a new device
        /// </summary>
        /// <param name="userAccessToken">User Access Token</param>
        /// <param name="deviceEntityToCreate">DeviceService Entity</param>
        /// <returns></returns>
        public string Create(string userAccessToken, DeviceEntityCreate deviceEntityToCreate)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var url = _baseUrl + "users/devices?key=" + userAccessToken;
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
           
            if(string.IsNullOrEmpty(deviceEntityToCreate.NativeId))
                throw new ApplicationException("Device NativeId is Required");

            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", 
                JsonConvert.SerializeObject(deviceEntityToCreate), 
                ParameterType.RequestBody);

            // execute the request
            IRestResponse response = client.Execute(request);
            var responseObject = (dynamic)JsonConvert.DeserializeObject(response.Content);

            if(responseObject!= null && responseObject.result != null)
                return responseObject.result.device_id;

            return string.Empty;
        }

        public DeviceEntityFetchRoot GetDevices(string userAccessToken)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var url = _baseUrl + "users/devices?key=" + userAccessToken;
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<DeviceEntityFetchRoot>(response.Content);
        }

        /// <summary>
        /// Delete a DeviceService by DeviceService Id
        /// </summary>
        /// <param name="userAccessToken">User Access Token</param>
        /// <param name="deviceId">DeviceService Id</param>
        /// <returns></returns>
        public string Delete(string userAccessToken, string deviceId)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(_baseUrl);
            var url = "users/devices?key=" + userAccessToken + "&device_id="+ deviceId;
            var request = new RestRequest(url, Method.POST);

            // execute the request
            IRestResponse response = client.Execute(request);
            return ExtractMessageFromResponse(response);
        }

        /// <summary>
        /// Delete Device AddressBook Contacts by UserAccessToken 
        /// and DeviceId
        /// </summary>
        /// <param name="userAccessToken">UserAccessToken</param>
        /// <param name="deviceId">DeviceId</param>
        public string DeleteDeviceAddressBookContacts(string userAccessToken, string deviceId)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(_baseUrl);
            var url = "devices/addressbooks?key=" + userAccessToken +
                      "&device_id=" + deviceId;

            var request = new RestRequest(url, Method.DELETE);
            IRestResponse response = client.Execute(request);
            return ExtractMessageFromResponse(response);
        }

        /// <summary>
        /// Fetch Device AddressBook Contacts by UserAccessToken 
        /// and DeviceId
        /// </summary>
        /// <param name="userAccessToken">UserAccessToken</param>
        /// <param name="deviceId">DeviceId</param>
        /// <returns>AddressBookResultRoot</returns>
        public AddressBookResultRoot GetDeviceAddressBookContacts(string userAccessToken, string deviceId)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(_baseUrl);
            var url = "devices/addressbooks?key=" + userAccessToken +
                      "&device_id=" + deviceId;

            var request = new RestRequest(url, Method.GET);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<AddressBookResultRoot>
                (response.Content);
        }

        /// <summary>
        /// Fetch SMS by Access Token and Device Id
        /// </summary>
        /// <param name="userAccessToken">User Access Token</param>
        /// <param name="deviceId">Device Id</param>
        /// <returns>DeviceMessage</returns>
        public DeviceMessage GetPendingMessages(string userAccessToken, string deviceId)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(_baseUrl);
            var url = "devices/messages?key=" + userAccessToken +
                      "&device_id=" + deviceId;

            var request = new RestRequest(url, Method.GET);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<DeviceMessage>
                (response.Content);
        }

        public string DeleteHandledMessages(string userAccessToken, string deviceId,
                    List<string> messageIds)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(_baseUrl);
            var url = "devices/messages?key=" + userAccessToken +
                      "&device_id=" + deviceId + "&messages=" +
                      JsonConvert.SerializeObject(messageIds);

            var request = new RestRequest(url, Method.DELETE);
     
            // execute the request
            IRestResponse response = client.Execute(request);
            return ExtractMessageFromResponse(response);
        }

        /// <summary>
        /// Send IM to the specified Device 
        /// </summary>
        /// <param name="userAccessToken">User AuserAccessTokenccess Token</param>
        /// <param name="deviceId">Device ID</param>
        /// <param name="message">Message in JSON</param>
        /// <returns>Success or Failure</returns>
        public string SendIm(string userAccessToken, string deviceId, string message)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(_baseUrl);
            var url = "devices/messages?key=" + userAccessToken +
                "&device_id=" + deviceId;

            var request = new RestRequest(url, Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", 
                message, ParameterType.RequestBody);

            // execute the request
            IRestResponse response = client.Execute(request);
            return ExtractMessageFromResponse(response);
        }
    }
}
