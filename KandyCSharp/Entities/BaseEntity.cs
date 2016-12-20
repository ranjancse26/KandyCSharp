using Newtonsoft.Json;

namespace KandyCSharp.Entities
{
    public class BaseEntity
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}