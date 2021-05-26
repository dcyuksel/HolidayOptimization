using HolidayOptimization.Domain.Entities;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HolidayOptimization.Application.UnitTests")]
namespace HolidayOptimization.Application.Queries.CountriesWithMostHoliday.Extensions
{
    public static class CountriesWithMostHolidayExtension
    {
        public static IEnumerable<string> GetCountriesWithMostHolidays(this IEnumerable<IEnumerable<PublicHoliday>> countriesPublicHolidays)
        {
            var countriesPublicHolidayCounts = GetCountriesTotalPublicHolidayCounts(countriesPublicHolidays);
            var countriesWithMostHoliday = countriesPublicHolidayCounts
                                            .MaxBy(pH => pH.publicHolidaysCount)
                                            .Select(c => c.countryCode)
                                            .ToList();

            return countriesWithMostHoliday;
        }

        internal static IEnumerable<(string countryCode, int publicHolidaysCount)> GetCountriesTotalPublicHolidayCounts(IEnumerable<IEnumerable<PublicHoliday>> countriesPublicHolidays)
        {
            var result = new List<(string countryCode, int publicHolidaysCount)>();

            foreach (var publicHolidays in countriesPublicHolidays)
            {
                if (!publicHolidays.Any())
                {
                    continue;
                }

                var model = (publicHolidays.First().CountryCode, publicHolidays.Count());
                result.Add(model);
            }

            return result;
        }
    }
}
