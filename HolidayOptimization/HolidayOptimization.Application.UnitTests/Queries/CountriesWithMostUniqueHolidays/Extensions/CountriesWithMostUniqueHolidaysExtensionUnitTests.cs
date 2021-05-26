using HolidayOptimization.Application.Queries.CountriesWithMostUniqueHolidays.Extensions;
using HolidayOptimization.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HolidayOptimization.Application.UnitTests.Queries.CountriesWithMostUniqueHolidays.Extensions
{
    public class CountriesWithMostUniqueHolidaysExtensionUnitTests
    {
        private List<List<PublicHoliday>> PublicHolidays;
        private List<List<PublicHoliday>> EmptyPublicHolidays;

        [SetUp]
        public void Setup()
        {
            var nlPublicHolidays = new List<PublicHoliday> { };
            var trPublicHolidays = new List<PublicHoliday> { };
            EmptyPublicHolidays = new List<List<PublicHoliday>> { nlPublicHolidays, trPublicHolidays };
            nlPublicHolidays = new List<PublicHoliday>
            {
                new PublicHoliday { CountryCode = "nl", Date = new DateTime(2021, 1, 1)},
                new PublicHoliday { CountryCode = "nl", Date = new DateTime(2021, 1, 2)},
                new PublicHoliday { CountryCode = "nl", Date = new DateTime(2021, 1, 3)},
            };
            trPublicHolidays = new List<PublicHoliday>
            {
                new PublicHoliday { CountryCode = "tr", Date = new DateTime(2021, 1, 4)},
                new PublicHoliday { CountryCode = "tr", Date = new DateTime(2021, 1, 5)},
                new PublicHoliday { CountryCode = "tr", Date = new DateTime(2021, 1, 6)},
                new PublicHoliday { CountryCode = "tr", Date = new DateTime(2021, 1, 7)},
            };
            PublicHolidays = new List<List<PublicHoliday>> { nlPublicHolidays, trPublicHolidays };
        }

        [Test]
        public void GetCountriesUniqueHoliday()
        {
            var expectedResult = new List<string> { "tr" };
            var result = PublicHolidays.GetCountriesUniqueHoliday();
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetCountriesUniqueHolidayWithEmpty()
        {
            var expectedResult = new List<string>();
            var result = EmptyPublicHolidays.GetCountriesUniqueHoliday();
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetCountriesUniqueHolidayCounts()
        {
            var expectedResult = new List<(string countryCode, int uniqueHolidayCount)> { ("nl", 3), ("tr", 4) };
            var counts = CountriesWithMostUniqueHolidaysExtension.GetDaysHolidayCounts(PublicHolidays);
            var result = CountriesWithMostUniqueHolidaysExtension.GetCountriesUniqueHolidayCounts(PublicHolidays, counts);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetCountriesUniqueHolidayCountsWithEmpty()
        {
            var expectedResult = new List<string>();
            var result = CountriesWithMostUniqueHolidaysExtension.GetCountriesUniqueHolidayCounts(EmptyPublicHolidays, new int[367]);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetDaysHolidayCounts()
        {
            var expectedResult = new int[367];
            expectedResult[1] = 1; expectedResult[2] = 1; expectedResult[3] = 1;
            expectedResult[4] = 1; expectedResult[5] = 1; expectedResult[6] = 1; expectedResult[7] = 1;
            var result = CountriesWithMostUniqueHolidaysExtension.GetDaysHolidayCounts(PublicHolidays);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetDaysHolidayCountsWithEmpty()
        {
            var expectedResult = new int[367];
            var result = CountriesWithMostUniqueHolidaysExtension.GetDaysHolidayCounts(EmptyPublicHolidays);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("0001-01-01 09:00:00", 1)]
        [TestCase("2021-01-02 09:00:00", 2)]
        [TestCase("9999-02-01 09:00:00", 32)]
        public void GetDayTest(string dateTimeAsString, int expectedResult)
        {
            var dateTime = DateTime.Parse(dateTimeAsString);
            var result = CountriesWithMostUniqueHolidaysExtension.GetDay(dateTime);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
