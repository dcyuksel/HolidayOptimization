using System;

namespace HolidayOptimization.Application.Queries.LongestHolidayAroundTheWorld.Extensions
{
    public static class DateTimeMuniteExtension
    {
        public static int GetMinute(DateTime dateTime)
        {
            var dayAsMinute = dateTime.DayOfYear * 24 * 60;
            var hourAsMinute = dateTime.Hour * 60;
            var minute = dateTime.Minute;

            return dayAsMinute + hourAsMinute + minute;
        }

        public static DateTime GetDateTimeFromYearAndMinute(int year, int totalMinutes)
        {
            var lastDay = new DateTime(year, 12, 31, 23, 59, 59);
            var maximumMinutes = GetMinute(lastDay);
            if (totalMinutes >= maximumMinutes)
            {
                return lastDay;
            }

            var firstDay = new DateTime(year, 1, 1, 0, 0, 0);
            if (totalMinutes <= 1440)
            {
                return firstDay;
            }

            totalMinutes -= 1440;

            var day = totalMinutes / (24 * 60);
            totalMinutes -= day * 24 * 60;
            firstDay = firstDay.AddDays(day);

            var hour = totalMinutes / 60;
            totalMinutes -= hour * 60;
            firstDay = firstDay.AddHours(hour);

            firstDay = firstDay.AddMinutes(totalMinutes);

            return firstDay;
        }
    }
}
