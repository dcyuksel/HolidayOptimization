using Newtonsoft.Json;

namespace HolidayOptimization.Domain.Entities
{
    public class Country
    {
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
