using System.Collections.Generic;
using System.Net;
using KandyCSharp.Entities;
using Newtonsoft.Json;
using RestSharp;

namespace KandyCSharp.Kandy
{
    public class GroupService : BaseService
    {
        private readonly string _baseUrl;
        private readonly string _accessToken;

        public GroupService(string accessToken)
        {
            _accessToken = accessToken;
            _baseUrl = Configuration.GetKandyBaseUrl();
        }

        /// <summary>
        ///  Create Group By Group Name and Image
        /// </summary>
        /// <param name="groupEntity">GroupEntity</param>
        /// <returns>Group ID</returns>
        public string CreateGroup(GroupEntity groupEntity)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var url = _baseUrl + "users/chatgroups?key=" + _accessToken;
            var client = new RestClient(url);

            var request = new RestRequest(Method.POST);
            request.AddParameter("undefined",
                JsonConvert.SerializeObject(groupEntity),
                ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            return ExtractMessageFromResponse(response);
        }

        /// <summary>
        /// Remove Group Members
        /// </summary>
        /// <param name="userAccessToken">UserAccessToken</param>
        /// <param name="groupId">GroupId</param>
        /// <param name="members">Colletion of Members</param>
        /// <returns>Success or Failure</returns>
        public string RemoveGroupMembers(string userAccessToken, string groupId, List<string> members)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var url = _baseUrl + "users/chatgroups/chatgroup/members?key=" + _accessToken +
                      "&group_id=" + groupId +
                      "&members= \"members\":" + JsonConvert.SerializeObject(members);

            var client = new RestClient(url);
            var request = new RestRequest(Method.DELETE);
            IRestResponse response = client.Execute(request);
            return ExtractMessageFromResponse(response);
        }

        /// <summary>
        /// Add Group Members 
        /// </summary>
        /// <param name="userAccessToken">UserAccessToken</param>
        /// <param name="groupId">GroupId</param>
        /// <param name="members">Colletion of Members</param>
        /// <returns>Success or Failure</returns>
        public string AddGroupMembers(string userAccessToken, string groupId, List<string> members)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var url = _baseUrl + "users/chatgroups/chatgroup/members?key=" + _accessToken +
                      "&group_id=" + groupId +
                      "&members= \"members\":" + JsonConvert.SerializeObject(members);

            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            IRestResponse response = client.Execute(request);
            return ExtractMessageFromResponse(response);
        }

        /// <summary>
        /// Fetch all groups
        /// </summary>
        /// <returns>Collection of Groups</returns>
        public AllGroupResultRootEntity GetAllGroups()
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(_baseUrl);
            var url = "users/chatgroups?key=" + _accessToken;

            var request = new RestRequest(url, Method.GET);
            IRestResponse response = client.Execute(request);
            if (response != null && response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<AllGroupResultRootEntity>
                    (response.Content);
            return null;
        }

        /// <summary>
        /// Find by Group ID
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <returns>Single Group Info by Group Id</returns>
        public GroupResultRootEntity FindGroupById(string groupId)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(_baseUrl);
            var url = "users/chatgroups/chatgroup?key="
                + _accessToken + "&group_id=" + groupId;

            var request = new RestRequest(url, Method.GET);
            IRestResponse response = client.Execute(request);
            if (response != null && response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<GroupResultRootEntity>
                    (response.Content);
            return null;
        }

        /// <summary>
        /// Send Group Message
        /// </summary>
        /// <param name="userAccessToken">User AuserAccessTokenccess Token</param>
        /// <param name="message">Message in JSON</param>
        /// <returns>Success or Failure</returns>
        public string SendGroupMessage(string userAccessToken, string message)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(_baseUrl);
            var url = "users/chatgroups/chatgroup/messages?key=" + userAccessToken;

            var request = new RestRequest(url, Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json",
                message, ParameterType.RequestBody);

            // execute the request
            IRestResponse response = client.Execute(request);
            return ExtractMessageFromResponse(response);
        }

        /// <summary>
        /// Destroy Group by Group ID
        /// </summary>
        /// <param name="groupId">Group Id</param>
        /// <returns>Success or not</returns>
        public string DestroyGroup(string groupId)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

            var client = new RestClient(_baseUrl);
            var url = "users/chatgroups/chatgroup?key="
                + _accessToken + "&group_id=" + groupId;

            var request = new RestRequest(url, Method.DELETE);
            IRestResponse response = client.Execute(request);
            return ExtractMessageFromResponse(response);
        }
    }
}
