using HolidayOptimization.Shared.Services;
using NUnit.Framework;

namespace HolidayOptimization.Shared.UnitTests.Services
{
    public class UrlServiceUnitTests
    {
        [Test]
        [TestCase("https://date.nager.at/api/v3/AvailableCountries")]
        public void GetAvailableCountriesUrlTest(string expectedResult)
        {
            var urlService = new UrlService();
            var result = urlService.GetAvailableCountriesUrl();
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("nl", "https://date.nager.at/api/v3/CountryInfo/nl")]
        [TestCase("tr", "https://date.nager.at/api/v3/CountryInfo/tr")]
        public void GetCountryInfoUrlTest(string countryCode, string expectedResult)
        {
            var urlService = new UrlService();
            var result = urlService.GetCountryInfoUrl(countryCode);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("nl", 2021, "https://date.nager.at/api/v3/PublicHolidays/2021/nl")]
        [TestCase("tr", 2020, "https://date.nager.at/api/v3/PublicHolidays/2020/tr")]
        public void GetPublicHolidaysUrlTest(string countryCode, int year, string expectedResult)
        {
            var urlService = new UrlService();
            var result = urlService.GetPublicHolidaysUrl(countryCode, year);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("https://restcountries.eu/rest/v2/all")]
        public void GetTimeZonesUrlTest(string expectedResult)
        {
            var urlService = new UrlService();
            var result = urlService.GetTimeZonesUrl();
            Assert.AreEqual(expectedResult, result);
        }
    }
}
