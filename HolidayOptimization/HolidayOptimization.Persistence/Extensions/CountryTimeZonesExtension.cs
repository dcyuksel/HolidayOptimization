using HolidayOptimization.Domain.Entities;
using System.Collections.Generic;

namespace HolidayOptimization.Persistence.Extensions
{
    public static class CountryTimeZonesExtension
    {
        public static Dictionary<string, IEnumerable<string>> ToCountryTimeZoneDictionary(this IEnumerable<CountryTimeZone> countryTimeZones)
        {
            var result = new Dictionary<string, IEnumerable<string>>();
            foreach (var countryTimeZone in countryTimeZones)
            {
                if (result.ContainsKey(countryTimeZone.CountryCode))
                {
                    continue;
                }

                result.Add(countryTimeZone.CountryCode, countryTimeZone.TimeZones);
            }

            return result;
        }
    }
}
