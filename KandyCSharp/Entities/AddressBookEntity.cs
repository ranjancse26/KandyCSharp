using System.Collections.Generic;
using Newtonsoft.Json;

namespace KandyCSharp.Entities
{
    public class AddressBookContact
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("contactId")]
        public string ContactId { get; set; }

        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("hintType")]
        public string HintType { get; set; }
    }

    public class AddressBookResult
    {
        [JsonProperty("contacts")]
        public List<AddressBookContact> Contacts { get; set; }
    }

    public class AddressBookResultRoot
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public AddressBookResult AddressBookResult { get; set; }
    }
}
