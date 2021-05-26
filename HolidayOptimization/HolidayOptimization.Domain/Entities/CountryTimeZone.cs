using Newtonsoft.Json;
using System.Collections.Generic;

namespace HolidayOptimization.Domain.Entities
{
    public class CountryTimeZone
    {
        [JsonProperty("alpha2Code")]
        public string CountryCode { get; set; }

        [JsonProperty("timezones")]
        public IEnumerable<string> TimeZones { get; set; }
    }
}
