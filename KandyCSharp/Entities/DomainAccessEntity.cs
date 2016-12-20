using Newtonsoft.Json;

namespace KandyCSharp.Entities
{
    public class DomainAccessTokenEntity
    {
        [JsonProperty("domain_access_token")]
        public string AccessToken { get; set; }
    }

    public class StatusDescription
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class DomainAccessEntity : BaseEntity
    {
        [JsonProperty("result")]
        public DomainAccessTokenEntity DomainAccess { get; set; }
      
        [JsonProperty("status_description")]
        public StatusDescription status_description { get; set; }
    }
}
