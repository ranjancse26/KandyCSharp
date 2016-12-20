using System.Collections.Generic;
using Newtonsoft.Json;

namespace KandyCSharp.Entities
{
    public class UserEntity
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("domain_name")]
        public string DomainName { get; set; }

        [JsonProperty("user_api_key")]
        public string APIKey { get; set; }

        [JsonProperty("user_api_secret")]
        public string APISecret { get; set; }

        [JsonProperty("user_first_name")]
        public string FirstName { get; set; }

        [JsonProperty("user_last_name")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [JsonProperty("user_phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("user_country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("full_user_id")]
        public string FullUserId { get; set; }

        [JsonProperty("user_password")]
        public string Password { get; set; }
    }

    public class UserResultEntity : BaseEntity
    {
        [JsonProperty("result")]
        public UserCollection Users { get; set; }
    }

    public class UserCollection
    {
        [JsonProperty("users")]
        public List<UserEntity> Users { get; set; }
    }
}
