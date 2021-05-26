using HolidayOptimization.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HolidayOptimization.Application.UnitTests")]
namespace HolidayOptimization.Application.Queries.MonthsWithMostHoliday.Extensions
{
    public static class MonthsWithMostHolidayExtension
    {
        public static IEnumerable<int> GetMonthsWithMostHolidays(this IEnumerable<IEnumerable<PublicHoliday>> countriesPublicHolidays)
        {
            var monthsHolidayCounts = GetMonthsHolidayCounts(countriesPublicHolidays);
            var monthsWithMostHolidays = GetMonthsWithMostHolidays(monthsHolidayCounts);

            return monthsWithMostHolidays;
        }

        internal static int[] GetMonthsHolidayCounts(IEnumerable<IEnumerable<PublicHoliday>> countriesPublicHolidays)
        {
            var monthsHolidayCounts = new int[13]; // month's total holiday count is stored at this array. Index zero is always zero.
            foreach (var publicHolidays in countriesPublicHolidays)
            {
                foreach (var publicHoliday in publicHolidays)
                {
                    var month = GetMonth(publicHoliday.Date);
                    monthsHolidayCounts[month]++;
                }
            }

            return monthsHolidayCounts;
        }

        internal static IEnumerable<int> GetMonthsWithMostHolidays(int[] monthsHolidayCounts)
        {
            // Below logic can be done using with only 1 loop but it requires additional operations.
            // Additional operations decrease the readability of the code and it is mind confusing.
            // Also array size is constant here, actually only 13 elements, performance is not a issue here.
            var maxHolidayCount = monthsHolidayCounts.Max();
            var months = new List<int>();
            for (var i = 1; i < monthsHolidayCounts.Length; i++)
            {
                if (maxHolidayCount == monthsHolidayCounts[i])
                {
                    months.Add(i);
                }
            }

            return months;
        }

        internal static int GetMonth(DateTime dateTime)
        {
            return dateTime.Month;
        }
    }
}
