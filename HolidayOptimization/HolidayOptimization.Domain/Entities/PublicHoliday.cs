using HolidayOptimization.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HolidayOptimization.Domain.Entities
{
    public class PublicHoliday
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("localName")]
        public string LocalName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("fixed")]
        public bool Fixed { get; set; }

        [JsonProperty("global")]
        public bool Global { get; set; }

        [JsonProperty("types")]
        public IEnumerable<PublicHolidayType> Types { get; set; }
    }
}
