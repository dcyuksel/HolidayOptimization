using HolidayOptimization.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HolidayOptimization.Application.UnitTests")]
namespace HolidayOptimization.Application.Queries.LongestHolidayAroundTheWorld.Extensions
{
    public static class LongestHolidayAroundTheWorldExtension
    {
        public static (DateTime StartTime, DateTime EndTime) GetLongestHolidayAroundTheWorld(
            this IEnumerable<IEnumerable<PublicHoliday>> countriesPublicHolidays,
            Dictionary<string, IEnumerable<string>> countryTimeZonesDictionary,
            int year)
        {
            var minutesHoliday = GetMinutesWithHolidays(countryTimeZonesDictionary, countriesPublicHolidays);
            var longestHoliday = GetLongestHoliday(minutesHoliday, year);

            return longestHoliday;
        }

        internal static bool[] GetMinutesWithHolidays(
            Dictionary<string, IEnumerable<string>> countryTimeZonesDictionary,
            IEnumerable<IEnumerable<PublicHoliday>> countriesPublicHolidays)
        {
            var minutesHoliday = new bool[367 * 24 * 60]; // stores minute is belong to a holiday or not.
            foreach (var publicHolidays in countriesPublicHolidays)
            {
                foreach (var publicHoliday in publicHolidays)
                {
                    var countryCode = publicHoliday.CountryCode;
                    var timeZones = countryTimeZonesDictionary[countryCode];
                    var minuteValues = GetMinuteValues(publicHoliday.Date, timeZones);
                    FillBetweenValues(minutesHoliday, minuteValues.startMinute, minuteValues.endMinute);
                }
            }

            return minutesHoliday;
        }

        internal static (DateTime StartTime, DateTime EndTime) GetLongestHoliday(bool[] minutesHoliday, int year)
        {
            var startMinute = 1440;
            var maximumCount = 0;
            var count = 0;

            for (var i = 1440; i < minutesHoliday.Length; i++)
            {
                var isMinuteHoliday = minutesHoliday[i];
                if (isMinuteHoliday)
                {
                    count++;
                }
                else if (count > maximumCount)
                {
                    startMinute = i - count;
                    maximumCount = count;
                }
                else
                {
                    count = 0;
                }
            }

            var startTime = DateTimeMuniteExtension.GetDateTimeFromYearAndMinute(year, startMinute);
            var endTime = DateTimeMuniteExtension.GetDateTimeFromYearAndMinute(year, startMinute + maximumCount);

            return (startTime, endTime);
        }

        internal static bool[] FillBetweenValues(bool[] array, int startMinute, int endMinute)
        {
            var length = endMinute > array.Length - 1 ? array.Length - 1 : endMinute;
            for (var i = startMinute; i <= length; i++)
            {
                array[i] = true;
            }

            return array;
        }

        internal static (int startMinute, int endMinute) GetMinuteValues(DateTime startDateTime, IEnumerable<string> timeZones)
        {
            if (!timeZones.Any())
            {
                return GetStartEndMinutesOfTimeZone(startDateTime, "UTC");
            }

            var firstTimeZone = timeZones.First();
            var (startMinute, endMinute) = GetStartEndMinutesOfTimeZone(startDateTime, firstTimeZone);

            for(var i = 1; i < timeZones.Count(); i++)
            {
                var (startMinute2, endMinute2) = GetStartEndMinutesOfTimeZone(startDateTime, timeZones.ElementAt(i));
                (startMinute, endMinute) = GetLargestTimeRange(startMinute, endMinute, startMinute2, endMinute2);
            }

            return (startMinute, endMinute);
        }

        internal static (int startMinute, int endMinute) GetLargestTimeRange(int startMinute, int endMinute, int startMinute2, int endMinute2)
        {
            if (startMinute2 < startMinute) startMinute = startMinute2;
            if (endMinute2 > endMinute) endMinute = endMinute2;

            return (startMinute, endMinute);
        }

        internal static (int startMinute, int endMinute) GetStartEndMinutesOfTimeZone(DateTime startDateTime, string timeZone)
        {
            var timeZoneDifference = TİmeZoneExtension.GetTimeZoneUtcMinutesDifference(timeZone);
            var startDateTimeAsMinute = DateTimeMuniteExtension.GetMinute(startDateTime) - timeZoneDifference;
            var endDateTime = IncreaseUpToOneDayInTheSameYear(startDateTime);
            var endTimeAsMinute = DateTimeMuniteExtension.GetMinute(endDateTime) - timeZoneDifference;
            startDateTimeAsMinute = startDateTimeAsMinute < 0 ? 0 : startDateTimeAsMinute;
            endTimeAsMinute = endTimeAsMinute > 367 * 24 * 60 - 1 ? 367 * 24 * 60 - 1 : endTimeAsMinute;

            return (startDateTimeAsMinute, endTimeAsMinute);
        }

        internal static DateTime IncreaseUpToOneDayInTheSameYear(DateTime dateTime)
        {
            var maximumPossibleDateTime = new DateTime(dateTime.Year, 12, 31, 23, 59, 59);
            var increasedDateTime = dateTime.AddDays(1);

            if(increasedDateTime.Year > dateTime.Year)
            {
                return maximumPossibleDateTime;
            }

            return increasedDateTime;
        }
    }
}
