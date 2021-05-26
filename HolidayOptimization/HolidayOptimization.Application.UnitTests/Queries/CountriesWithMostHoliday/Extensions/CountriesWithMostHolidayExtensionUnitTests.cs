using HolidayOptimization.Application.Queries.CountriesWithMostHoliday.Extensions;
using HolidayOptimization.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HolidayOptimization.Application.UnitTests.Queries.CountriesWithMostHoliday.Extensions
{
    public class CountriesWithMostHolidayExtensionUnitTests
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
                new PublicHoliday { CountryCode = "tr", Date = new DateTime(2021, 2, 3)},
                new PublicHoliday { CountryCode = "tr", Date = new DateTime(2021, 2, 4)},
                new PublicHoliday { CountryCode = "tr", Date = new DateTime(2021, 2, 5)},
                new PublicHoliday { CountryCode = "tr", Date = new DateTime(2021, 3, 6)},
            };
            PublicHolidays = new List<List<PublicHoliday>> { nlPublicHolidays, trPublicHolidays };
        }

        [Test]
        public void GetCountriesWithMostHolidays()
        {
            var expectedResult = new List<string> { "tr" };
            var result = PublicHolidays.GetCountriesWithMostHolidays();
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetCountriesWithMostHolidaysWithEmpty()
        {
            var expectedResult = new List<string> { };
            var result = CountriesWithMostHolidayExtension.GetCountriesTotalPublicHolidayCounts(EmptyPublicHolidays);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetCountriesTotalPublicHolidayCounts()
        {
            var expectedResult = new List<(string countryCode, int publicHolidaysCount)> { ("nl", 3), ("tr", 4) };
            var result = CountriesWithMostHolidayExtension.GetCountriesTotalPublicHolidayCounts(PublicHolidays);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetCountriesTotalPublicHolidayCountsWithEmpty()
        {
            var expectedResult = new List<(string countryCode, int publicHolidaysCount)> { };
            var result = CountriesWithMostHolidayExtension.GetCountriesTotalPublicHolidayCounts(EmptyPublicHolidays);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
