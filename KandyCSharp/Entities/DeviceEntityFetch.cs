using System.Collections.Generic;
using Newtonsoft.Json;

namespace KandyCSharp.Entities
{
    public class DeviceEntityFetch
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("nativeID")]
        public string NativeID { get; set; }

        [JsonProperty("family")]
        public string Family { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("osVersion")]
        public string OSVersion { get; set; }

        [JsonProperty("clientVersion")]
        public string ClientVersion { get; set; }

        [JsonProperty("registrationTimestamp")]
        public string RegistrationTimestamp { get; set; }
    }

    public class DeviceEntityFetchResult
    {
        [JsonProperty("devices")]
        public List<DeviceEntityFetch> Devices { get; set; }
    }

    public class DeviceEntityFetchRoot : BaseEntity
    {
        [JsonProperty("result")]
        public DeviceEntityFetchResult DeviceEntityFetchResult { get; set; }

        [JsonProperty("status_description")]
        public StatusDescription StatusDescription { get; set; }
    }
}
