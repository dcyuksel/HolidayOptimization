using HolidayOptimization.Domain.Entities;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HolidayOptimization.Application.UnitTests")]
namespace HolidayOptimization.Application.Queries.CountriesWithMostUniqueHolidays.Extensions
{
    public static class CountriesWithMostUniqueHolidaysExtension
    {
        public static IEnumerable<string> GetCountriesUniqueHoliday(this IEnumerable<IEnumerable<PublicHoliday>> countriesPublicHolidays)
        {
            var daysHolidayCounts = GetDaysHolidayCounts(countriesPublicHolidays);
            var countriesUniqueHolidayCounts = GetCountriesUniqueHolidayCounts(countriesPublicHolidays, daysHolidayCounts);
            var countriesWithMostUniqueHolidays = countriesUniqueHolidayCounts
                                                    .MaxBy(pH => pH.uniqueHolidayCount)
                                                    .Select(pH => pH.countryCode)
                                                    .ToList();

            return countriesWithMostUniqueHolidays;
        }

        internal static IEnumerable<(string countryCode, int uniqueHolidayCount)> GetCountriesUniqueHolidayCounts(
            IEnumerable<IEnumerable<PublicHoliday>> countriesPublicHolidays,
            int[] daysHolidayCounts)
        {
            var result = new List<(string countryCode, int uniqueHolidayCount)>();
            foreach (var publicHolidays in countriesPublicHolidays)
            {
                if (!publicHolidays.Any())
                {
                    continue;
                }
                var uniqueHolidayCount = 0;
                foreach (var publicHoliday in publicHolidays)
                {
                    var day = GetDay(publicHoliday.Date);
                    if (daysHolidayCounts[day] == 1)
                    {
                        uniqueHolidayCount++;
                    }
                }

                var model = (publicHolidays.First().CountryCode, uniqueHolidayCount);
                result.Add(model);
            }

            return result;
        }

        internal static int[] GetDaysHolidayCounts(IEnumerable<IEnumerable<PublicHoliday>> countriesPublicHolidays)
        {
            var daysHolidayCounts = new int[367]; // day's total holiday count is stored at this array. Index zero is always zero.
            foreach (var publicHolidays in countriesPublicHolidays)
            {
                foreach (var publicHoliday in publicHolidays)
                {

                    var day = GetDay(publicHoliday.Date);
                    daysHolidayCounts[day]++;
                }
            }

            return daysHolidayCounts;
        }

        internal static int GetDay(DateTime dateTime)
        {
            return dateTime.DayOfYear;
        }
    }
}
