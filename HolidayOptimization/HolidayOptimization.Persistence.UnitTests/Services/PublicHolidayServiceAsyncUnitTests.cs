using HolidayOptimization.Application.Interfaces.Shared;
using HolidayOptimization.Domain.Entities;
using HolidayOptimization.Persistence.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolidayOptimization.Persistence.UnitTests.Services
{
    public class PublicHolidayServiceAsyncUnitTests
    {
        private Mock<IHttpClientWrapperAsync<IEnumerable<PublicHoliday>>> mockhttpClientWrapperAsync;
        private Mock<IUrlService> mockUrlService;
        private readonly string CountryCode = "nl";
        private readonly string Url = "url";
        private readonly IEnumerable<string> Urls = new List<string> { "url", "url" }; 
        private readonly int Year = 2021;
        private readonly IEnumerable<PublicHoliday> PublicHolidays = new List<PublicHoliday> { new PublicHoliday { Name = "Netherlands", CountryCode = "nl" } };

        [SetUp]
        public void Setup()
        {
            var mockhttpClientWrapperAsync = new Mock<IHttpClientWrapperAsync<IEnumerable<PublicHoliday>>>();
            mockhttpClientWrapperAsync.Setup(p => p.GetAsync(Url)).ReturnsAsync(PublicHolidays);
            mockhttpClientWrapperAsync.Setup(p => p.GetMultipleAsync(Urls)).ReturnsAsync(new List<IEnumerable<PublicHoliday>> { PublicHolidays , PublicHolidays });
            this.mockhttpClientWrapperAsync = mockhttpClientWrapperAsync;

            var mockUrlService = new Mock<IUrlService>();
            mockUrlService.Setup(p => p.GetAvailableCountriesUrl()).Returns(Url);
            mockUrlService.Setup(p => p.GetCountryInfoUrl(CountryCode)).Returns(Url);
            mockUrlService.Setup(p => p.GetPublicHolidaysUrl(CountryCode, Year)).Returns(Url);
            mockUrlService.Setup(p => p.GetTimeZonesUrl()).Returns(Url);
            this.mockUrlService = mockUrlService;
        }

        [Test]
        public async Task GetPublicHolidaysTest()
        {
            var publicHolidayService = new PublicHolidayServiceAsync(mockhttpClientWrapperAsync.Object, mockUrlService.Object);
            var result = await publicHolidayService.GetPublicHolidays(CountryCode, Year);
            Assert.AreEqual(PublicHolidays, result);
        }

        [Test]
        public async Task GetPublicHolidaysOfCountriesTest()
        {
            var publicHolidayService = new PublicHolidayServiceAsync(mockhttpClientWrapperAsync.Object, mockUrlService.Object);
            var country = new Country { CountryCode = CountryCode, Name = "Netherlands" };
            var result = await publicHolidayService.GetPublicHolidaysOfCountries(new List<Country> { country, country }, Year);
            var expectedResult = new List<IEnumerable<PublicHoliday>> { PublicHolidays, PublicHolidays };
            Assert.AreEqual(expectedResult, result);
        }
    }
}
