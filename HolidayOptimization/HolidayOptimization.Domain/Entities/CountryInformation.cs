using Newtonsoft.Json;
using System.Collections.Generic;

namespace HolidayOptimization.Domain.Entities
{
    public class CountryInformation
    {
        [JsonProperty("commonName")]
        public string CommonName { get; set; }

        [JsonProperty("officialName")]
        public string OfficialName { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("borders")]
        public IEnumerable<Country> Borders { get; set; }
    }
}
