using System.Collections.Generic;
using Newtonsoft.Json;

namespace KandyCSharp.Entities
{
    public class DeviceEntityCreate
    {
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }

        [JsonProperty("device_native_id")]
        public string NativeId { get; set; }

        [JsonProperty("device_family")]
        public string Family { get; set; }

        [JsonProperty("device_name")]
        public string Name { get; set; }

        [JsonProperty("client_sw_version")]
        public string ClientSoftwareVersion { get; set; }

        [JsonProperty("device_os_version")]
        public string OperatingSystemVersion { get; set; }
    }

    public class DeviceResultCreate
    {
        [JsonProperty("result")]
        public DeviceCollectionCreate Devices { get; set; }
    }

    public class DeviceCollectionCreate
    {
        [JsonProperty("devices")]
        public List<DeviceEntityCreate> Devices { get; set; }
    }
}
