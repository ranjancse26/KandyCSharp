using System.Collections.Generic;
using Newtonsoft.Json;

namespace KandyCSharp.Entities
{
    public class DomainEntity
    {
        [JsonProperty("domain_api_key")]
        public string APIKey { get; set; }

        [JsonProperty("domain_api_secret")]
        public string APISecret { get; set; }

        [JsonProperty("domain_name")]
        public string DomainName { get; set; }

        [JsonProperty("project_name")]
        public string ProjectName { get; set; }

        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("account_email")]
        public string AccountEmail { get; set; }

        [JsonProperty("number_of_users")]
        public string NumberOfUsers { get; set; }

        [JsonProperty("next_recurring_payment")]
        public string NextRecurringPayment { get; set; }
    }

    public class DomainResultEntity : BaseEntity
    {
        [JsonProperty("result")]
        public DomainsCollection Domains { get; set; }
    }

    public class DomainsCollection
    {
        [JsonProperty("domains")]
        public List<DomainEntity> Domains { get; set; }
    }
}
