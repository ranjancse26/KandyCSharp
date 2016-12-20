using System.Collections.Generic;
using Newtonsoft.Json;

namespace KandyCSharp.Entities
{
    public class ContactEntity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("home_phone")]
        public string HomePhone { get; set; }

        [JsonProperty("mobile_number")]
        public string MobileNumber { get; set; }

        [JsonProperty("business_number")]
        public string BusinessNumber { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
   
    [JsonObject("result")]
    public class ContactResultEntity 
    {
        [JsonProperty("contacts")]
        public List<ContactEntity> Contacts { get; set; }
    }

    public class ContactResultRootEntity : BaseEntity
    {
        [JsonProperty("result")]
        public ContactResultEntity ContactResultEntity { get; set; }

        [JsonProperty("status_description")]
        public StatusDescription StatusDescription { get; set; }
    }
}
