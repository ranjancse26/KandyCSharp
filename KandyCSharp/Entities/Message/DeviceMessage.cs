using Newtonsoft.Json;
using System.Collections.Generic;

namespace KandyCSharp.Entities.Message
{
    public class Sender
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("domain_name")]
        public string DomainName { get; set; }

        [JsonProperty("full_user_id")]
        public string FullUserId { get; set; }
    }

    public class Message
    {
        [JsonProperty("mimeType")]
        public string MimeType { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class MessageRoot
    {
        [JsonProperty("messageType")]
        public string MessageType { get; set; }

        [JsonProperty("sender")]
        public Sender Sender { get; set; }

        [JsonProperty("UUID")]
        public string UUID { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }
    }

    public class Result
    {
        [JsonProperty("messages")]
        public List<MessageRoot> Messages { get; set; }
    }

    public class DeviceMessage
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public Result Result { get; set; }
    }
    
    public class MessageToSend
    {
        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("UUID")]
        public string UUID { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }
    }

    public class MessageToSendRoot
    {
        [JsonProperty("message")]
        public MessageToSend MessageToSend { get; set; }
    }

    public class GroupMessageToSend : MessageToSend
    {
        [JsonProperty("group_id")]
        public string GroupId { get; set; }
    }

    public class GroupMessageToSendRoot
    {
        [JsonProperty("message")]
        public GroupMessageToSend MessageToSend { get; set; }
    }
}
