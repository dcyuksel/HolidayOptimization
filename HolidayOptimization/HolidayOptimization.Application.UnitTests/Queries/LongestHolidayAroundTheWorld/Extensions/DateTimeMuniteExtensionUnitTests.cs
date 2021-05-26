using HolidayOptimization.Application.Queries.LongestHolidayAroundTheWorld.Extensions;
using NUnit.Framework;
using System;

namespace HolidayOptimization.Application.UnitTests.Queries.LongestHolidayAroundTheWorld.Extensions
{
    class DateTimeMuniteExtensionUnitTests
    {
        [Test]
        [TestCase("2021-01-01 00:00:00", 24 * 60)]
        [TestCase("2021-01-01 00:01:00", 24 * 60 + 1)]
        [TestCase("2021-01-01 01:00:00", 24 * 60 + 60)]
        [TestCase("2021-01-02 00:00:00", 24 * 60 * 2)]
        [TestCase("2021-02-01 00:00:00", 24 * 60 * 32)]
        [TestCase("2021-12-31 23:59:59", 366 * 24 * 60 - 1)]
        public void GetMinuteTest(string dateTimeAsString, int expectedResult)
        {
            var dateTime = DateTime.Parse(dateTimeAsString);
            var result = DateTimeMuniteExtension.GetMinute(dateTime);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(2021, -10, "2021-01-01 00:00:00")]
        [TestCase(2021, 0, "2021-01-01 00:00:00")]
        [TestCase(2021, 1440, "2021-01-01 00:00:00")]
        [TestCase(2021, 1441, "2021-01-01 00:01:00")]
        [TestCase(2021, 24 * 60 * 2, "2021-01-02 00:00:00")]
        [TestCase(2021, 24 * 60 * 32, "2021-02-01 00:00:00")]
        [TestCase(2021, 366 * 24 * 60 - 1, "2021-12-31 23:59:59")]
        [TestCase(2021, 370 * 24 * 60, "2021-12-31 23:59:59")]
        public void GetDateTimeFromYearAndMinuteTest(int year, int minute, string expectedResult)
        {
            var expectedDateTime = DateTime.Parse(expectedResult);
            var result = DateTimeMuniteExtension.GetDateTimeFromYearAndMinute(year, minute);
            Assert.AreEqual(expectedDateTime, result);
        }
    }
}
