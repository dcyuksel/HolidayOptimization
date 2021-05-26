using HolidayOptimization.Domain.Entities;
using HolidayOptimization.Persistence.Extensions;
using MoreLinq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace HolidayOptimization.Persistence.UnitTests.Extensions
{
    public class CountryTimeZonesExtensionUnitTests
    {
        private static readonly List<List<CountryTimeZone>> TestCases = new List<List<CountryTimeZone>>
        {
            { new List<CountryTimeZone>{ } },
            { new List<CountryTimeZone>{ new CountryTimeZone{ CountryCode = "nl", TimeZones = new List<string> {"UTC+01:00" } } } }, 
            { new List<CountryTimeZone>{ new CountryTimeZone{ CountryCode = "nl", TimeZones = new List<string> {"UTC+01:00" } } , new CountryTimeZone { CountryCode = "nl", TimeZones = new List<string> { "UTC+01:00" } } } }, 
            { new List<CountryTimeZone>{ new CountryTimeZone{ CountryCode = "nl", TimeZones = new List<string> {"UTC+01:00" } } , new CountryTimeZone { CountryCode = "tr", TimeZones = new List<string> { "UTC+03:00" } } } }, 
        }; 

        [Test]
        public void ToCountryTimeZoneDictionaryTest()
        {
            foreach(var testCase in TestCases)
            {
                var countryTimeZoneDictionary = testCase.ToCountryTimeZoneDictionary();
                var uniqueCountryCount = testCase.DistinctBy(c => c.CountryCode).Count();
                Assert.AreEqual(countryTimeZoneDictionary.Count, uniqueCountryCount);
            }
        }
    }
}
