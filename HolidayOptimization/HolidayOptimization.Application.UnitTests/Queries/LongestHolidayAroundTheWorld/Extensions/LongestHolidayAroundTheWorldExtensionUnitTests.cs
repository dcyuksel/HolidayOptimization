using HolidayOptimization.Application.Queries.CountriesWithMostHoliday.Extensions;
using HolidayOptimization.Application.Queries.LongestHolidayAroundTheWorld.Extensions;
using HolidayOptimization.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HolidayOptimization.Application.UnitTests.Queries.LongestHolidayAroundTheWorld.Extensions
{
    public class LongestHolidayAroundTheWorldExtensionUnitTests
    {
        private Dictionary<string, IEnumerable<string>> CountryTimeZonesDictionary;
        private IEnumerable<string> TimeZones;
        
        [SetUp]
        public void Setup()
        {
            CountryTimeZonesDictionary = new Dictionary<string, IEnumerable<string>>
            {
                { "tr", new List<string>{ "UTC+03:00" } },
                { "nl", new List<string>{ "UTC+01:00", "UTC+04:00" } },
            };

            TimeZones = new List<string> { "UTC+01:00", "UTC+03:00" };
        }

        [Test]
        public void GetLongestHolidayAroundTheWorld()
        {
            var nlPublicHolidays = new List<PublicHoliday>
            {
                new PublicHoliday { CountryCode = "nl", Date = new DateTime(2021, 1, 1)},
            };
            var trPublicHolidays = new List<PublicHoliday>
            {
                new PublicHoliday { CountryCode = "tr", Date = new DateTime(2021, 1, 1)},
            };
            var publicHolidays = new List<List<PublicHoliday>> { nlPublicHolidays, trPublicHolidays };

            var expectedStartTime = DateTime.Parse("2021-01-01 00:00:00");
            var expectedEndTime = DateTime.Parse("2021-01-01 23:01:00");

            var expectedResult = (expectedStartTime, expectedEndTime);
            var result = publicHolidays.GetLongestHolidayAroundTheWorld(CountryTimeZonesDictionary, 2021);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetLongestHolidayAroundTheWorldEmptyTest()
        {
            var nlPublicHolidays = new List<PublicHoliday> { };
            var trPublicHolidays = new List<PublicHoliday> { };
            var emptyPublicHolidays = new List<List<PublicHoliday>> { nlPublicHolidays, trPublicHolidays };

            var expectedStartTime = DateTime.Parse("2021-01-01 00:00:00");
            var expectedEndTime = DateTime.Parse("2021-01-01 00:00:00");

            var expectedResult = (expectedStartTime, expectedEndTime); 
            var result = emptyPublicHolidays.GetLongestHolidayAroundTheWorld(CountryTimeZonesDictionary, 2021);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetMinutesWithHolidaysTest()
        {
            var nlPublicHolidays = new List<PublicHoliday>
            {
                new PublicHoliday { CountryCode = "nl", Date = new DateTime(2021, 1, 1)},
            };
            var trPublicHolidays = new List<PublicHoliday>
            {
                new PublicHoliday { CountryCode = "tr", Date = new DateTime(2021, 1, 1)},
            };
            var publicHolidays = new List<List<PublicHoliday>> { nlPublicHolidays, trPublicHolidays };

            var expectedResult = new bool[367 * 24 * 60];
            expectedResult = LongestHolidayAroundTheWorldExtension.FillBetweenValues(expectedResult, 1440 - 240, 2880 - 60);

            var result = LongestHolidayAroundTheWorldExtension.GetMinutesWithHolidays(CountryTimeZonesDictionary, publicHolidays);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetMinutesWithHolidaysEmptyTest()
        {
            var nlPublicHolidays = new List<PublicHoliday> { };
            var trPublicHolidays = new List<PublicHoliday> { };
            var emptyPublicHolidays = new List<List<PublicHoliday>> { nlPublicHolidays, trPublicHolidays };

            var expectedResult = new bool[367 * 24 * 60];
            var result = LongestHolidayAroundTheWorldExtension.GetMinutesWithHolidays(CountryTimeZonesDictionary, emptyPublicHolidays);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(2021, 0, 1440, 1440 * 15, 1440 * 15, "2021-01-01 00:00:00", "2021-01-01 00:01:00")]
        [TestCase(2021, 1440, 1440 * 3, 1440 * 15, 1440 * 16, "2021-01-01 00:00:00", "2021-01-03 00:01:00")]
        [TestCase(2021, 1440, 1440 * 3, 1440 * 15, 1440 * 17, "2021-01-01 00:00:00", "2021-01-03 00:01:00")]
        [TestCase(2021, 1440, 1440 * 3, 1440 * 15, 1440 * 20, "2021-01-15 00:00:00", "2021-01-20 00:01:00")]
        [TestCase(2021, 1440, 1440 * 3, 360 * 24 * 60, 369 * 24 * 60, "2021-01-01 00:00:00", "2021-01-03 00:01:00")]
        public void GetLongestHolidayTest(int year, int start1, int end1, int start2, int end2, string expectedStartTimeAsString, string expectedEndTimeAsString)
        {
            var expectedStartTime = DateTime.Parse(expectedStartTimeAsString);
            var expectedEndTime = DateTime.Parse(expectedEndTimeAsString);
            var expectedResult = (expectedStartTime, expectedEndTime);

            var minutesHoliday = new bool[367 * 24 * 60];
            minutesHoliday = LongestHolidayAroundTheWorldExtension.FillBetweenValues(minutesHoliday, start1, end1);
            minutesHoliday = LongestHolidayAroundTheWorldExtension.FillBetweenValues(minutesHoliday, start2, end2);

            var result = LongestHolidayAroundTheWorldExtension.GetLongestHoliday(minutesHoliday, year);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(new bool[] { false, false, false, false, false }, 0, 0, new bool[] { true, false, false, false, false })]
        [TestCase(new bool[] { false, false, false, false, false }, 0, 1, new bool[] { true, true, false, false, false })]
        [TestCase(new bool[] { false, false, false, false, false }, 0, 2, new bool[] { true, true, true, false, false })]
        [TestCase(new bool[] { false, false, false, false, false }, 0, 3, new bool[] { true, true, true, true, false })]
        [TestCase(new bool[] { false, false, false, false, false }, 0, 4, new bool[] { true, true, true, true, true })]
        [TestCase(new bool[] { false, false, false, false, false }, 0, 5, new bool[] { true, true, true, true, true })]
        public void FillBetweenValuesTest(bool[] array, int startMinute, int endMinute, bool[] expectedResult)
        {
            var result = LongestHolidayAroundTheWorldExtension.FillBetweenValues(array, startMinute, endMinute);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("2021-01-01 00:00:00", 1260, 2820)]
        [TestCase("2021-12-31 20:59:59", 526679, 526979)]
        [TestCase("2021-12-31 23:59:59", 526859, 526979)]
        public void GetMinuteValuesTest(string dateTimeAsString, int startMinute, int endMinute)
        {
            var dateTime = DateTime.Parse(dateTimeAsString);
            var expectedResult = (startMinute, endMinute);
            var result = LongestHolidayAroundTheWorldExtension.GetMinuteValues(dateTime, TimeZones);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(0, 10, 2, 8, 0, 10)]
        [TestCase(3, 10, 1, 14, 1, 14)]
        [TestCase(2, 5, 3, 7, 2, 7)]
        public void GetLargestTimeRangeTest(int startMinute, int endMinute, int startMinute2, int endMinute2, int expectedStartMinute, int expectedEndMinute)
        {
            var expectedResult = (expectedStartMinute, expectedEndMinute);
            var result = LongestHolidayAroundTheWorldExtension.GetLargestTimeRange(startMinute, endMinute, startMinute2, endMinute2);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("2021-01-01 00:00:00", "UTC+01:00", 1380, 2820)]
        [TestCase("2021-12-31 20:59:59", "UTC", 526859, 527039)]
        [TestCase("2021-12-31 23:59:59", "UTC-01:00", 527099, 527099)]
        public void GetStartEndMinutesOfTimeZoneTest(string startDateTimeAsString, string timeZone, int startMinute, int endMinute)
        {
            var startDateTime = DateTime.Parse(startDateTimeAsString);
            var expectedResult = (startMinute, endMinute);
            var result = LongestHolidayAroundTheWorldExtension.GetStartEndMinutesOfTimeZone(startDateTime, timeZone);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("2021-01-01 00:00:00", "2021-01-02 00:00:00")]
        [TestCase("2021-12-31 20:59:59", "2021-12-31 23:59:59")]
        [TestCase("2021-12-31 23:59:59", "2021-12-31 23:59:59")]
        public void IncreaseUpToOneDayInTheSameYearTest(string dateTimeAsString, string increasedDateTimeAsString)
        {
            var dateTime = DateTime.Parse(dateTimeAsString);
            var expectedResult = DateTime.Parse(increasedDateTimeAsString);
            var result = LongestHolidayAroundTheWorldExtension.IncreaseUpToOneDayInTheSameYear(dateTime);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
