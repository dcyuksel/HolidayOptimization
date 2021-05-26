using HolidayOptimization.Application.Queries.MonthsWithMostHoliday.Extensions;
using HolidayOptimization.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HolidayOptimization.Application.UnitTests.Queries.MonthsWithMostHoliday.Extensions
{
    public class MonthsWithMostHolidayExtensionUnitTests
    {
        private List<List<PublicHoliday>> PublicHolidays;

        [SetUp]
        public void Setup()
        {
            var nlPublicHolidays = new List<PublicHoliday>
            {
                new PublicHoliday { CountryCode = "nl", Date = new DateTime(2021, 1, 1)},
                new PublicHoliday { CountryCode = "nl", Date = new DateTime(2021, 1, 2)},
                new PublicHoliday { CountryCode = "nl", Date = new DateTime(2021, 1, 3)},
            };
            var trPublicHolidays = new List<PublicHoliday>
            {
                new PublicHoliday { CountryCode = "tr", Date = new DateTime(2021, 2, 3)},
                new PublicHoliday { CountryCode = "tr", Date = new DateTime(2021, 2, 4)},
                new PublicHoliday { CountryCode = "tr", Date = new DateTime(2021, 2, 5)},
                new PublicHoliday { CountryCode = "tr", Date = new DateTime(2021, 3, 3)},
            };
            this.PublicHolidays = new List<List<PublicHoliday>> { nlPublicHolidays, trPublicHolidays };
        }

        [Test]
        public void GetMonthsWithMostHolidays()
        {
            var expectedResult = new List<int> { 1, 2 };
            var result = PublicHolidays.GetMonthsWithMostHolidays();
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetMonthsHolidayCounts()
        {
            var expectedResult = new int[] { 0, 3, 3, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var result = MonthsWithMostHolidayExtension.GetMonthsHolidayCounts(PublicHolidays);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(new int[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 }, new int[] { 12 })]
        [TestCase(new int[] { 0, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 }, new int[] { 1, 12 })]
        [TestCase(new int[] { 0, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 }, new int[] { 1, 2, 12 })]
        [TestCase(new int[] { 0, 2, 2, 1, 1, 3, 1, 1, 1, 1, 1, 1, 2 }, new int[] { 5 })]
        [TestCase(new int[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 })]
        [TestCase(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 })]
        public void GetMonthsWithMostHolidays(int[] monthsHolidayCounts, int[] months)
        {
            var expectedResult = months.ToList();
            var result = MonthsWithMostHolidayExtension.GetMonthsWithMostHolidays(monthsHolidayCounts);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("0001-01-01 09:00:00", 1)]
        [TestCase("2021-02-02 09:00:00", 2)]
        [TestCase("9999-12-01 09:00:00", 12)]
        public void GetMonthTest(string dateTimeAsString, int expectedResult)
        {
            var dateTime = DateTime.Parse(dateTimeAsString);
            var result = MonthsWithMostHolidayExtension.GetMonth(dateTime);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
