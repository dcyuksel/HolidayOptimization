using HolidayOptimization.Application.Interfaces.Shared;
using HolidayOptimization.Domain.Entities;
using HolidayOptimization.Persistence.Services;
using Moq;
using MoreLinq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayOptimization.Persistence.UnitTests.Services
{
    public class CountryServiceAsyncUnitTests
    {
        private Mock<IHttpClientWrapperAsync<CountryInformation>> mockCountryInformationHttpClientWrapperAsync;
        private Mock<IHttpClientWrapperAsync<IEnumerable<Country>>> mockCountriesHttpClientWrapperAsync;
        private Mock<IHttpClientWrapperAsync<IEnumerable<CountryTimeZone>>> mockCountriesTimeZoneHttpClientWrapperAsync;
        private Mock<IUrlService> mockUrlService;
        private readonly string CountryCode = "nl";
        private readonly string Url = "url";
        private readonly int Year = 2021;
        private readonly CountryInformation NlCountryInformation = new CountryInformation { CountryCode = "nl", OfficialName = "Netherlands", CommonName = "netherlands" };
        private readonly IEnumerable<Country> AvailableCountries = new List<Country> { new Country { Name = "Netherlands", CountryCode = "nl" }, new Country { Name = "Turkey", CountryCode = "tr" } };
        private readonly IEnumerable<CountryTimeZone> TimeZones = new List<CountryTimeZone> { new CountryTimeZone { CountryCode = "nl", TimeZones = new List<string> { "UTC+01:00", "UTC+04:00" } }, new CountryTimeZone { CountryCode = "tr", TimeZones = new List<string> { "UTC+03:00" } } };

        [SetUp]
        public void Setup()
        {
            var mockCountryInformationHttpClientWrapperAsync = new Mock<IHttpClientWrapperAsync<CountryInformation>>();
            mockCountryInformationHttpClientWrapperAsync.Setup(p => p.GetAsync(Url)).ReturnsAsync(NlCountryInformation);
            this.mockCountryInformationHttpClientWrapperAsync = mockCountryInformationHttpClientWrapperAsync;

            var mockCountriesHttpClientWrapperAsync = new Mock<IHttpClientWrapperAsync<IEnumerable<Country>>>();
            mockCountriesHttpClientWrapperAsync.Setup(p => p.GetAsync(Url)).ReturnsAsync(AvailableCountries);
            this.mockCountriesHttpClientWrapperAsync = mockCountriesHttpClientWrapperAsync;

            var mockCountriesTimeZoneHttpClientWrapperAsync = new Mock<IHttpClientWrapperAsync<IEnumerable<CountryTimeZone>>>();
            mockCountriesTimeZoneHttpClientWrapperAsync.Setup(p => p.GetAsync(Url)).ReturnsAsync(TimeZones);
            this.mockCountriesTimeZoneHttpClientWrapperAsync = mockCountriesTimeZoneHttpClientWrapperAsync;

            var mockUrlService = new Mock<IUrlService>();
            mockUrlService.Setup(p => p.GetAvailableCountriesUrl()).Returns(Url);
            mockUrlService.Setup(p => p.GetCountryInfoUrl(CountryCode)).Returns(Url);
            mockUrlService.Setup(p => p.GetPublicHolidaysUrl(CountryCode, Year)).Returns(Url);
            mockUrlService.Setup(p => p.GetTimeZonesUrl()).Returns(Url);
            this.mockUrlService = mockUrlService;
        }

        [Test]
        public async Task GetCountryInformationAsyncTest()
        {
            var countryService = new CountryServiceAsync(mockCountryInformationHttpClientWrapperAsync.Object, mockCountriesHttpClientWrapperAsync.Object, mockCountriesTimeZoneHttpClientWrapperAsync.Object, mockUrlService.Object);
            var result = await countryService.GetCountryInformationAsync(CountryCode);
            Assert.AreEqual(NlCountryInformation, result);
        }

        [Test]
        public async Task GetAvailableCountriesAsyncTest()
        {
            var countryService = new CountryServiceAsync(mockCountryInformationHttpClientWrapperAsync.Object, mockCountriesHttpClientWrapperAsync.Object, mockCountriesTimeZoneHttpClientWrapperAsync.Object, mockUrlService.Object);
            var result = await countryService.GetAvailableCountriesAsync();
            Assert.AreEqual(AvailableCountries, result);
        }

        [Test]
        public async Task GetCountryTimeZoneDictionaryTest()
        {
            var countryService = new CountryServiceAsync(mockCountryInformationHttpClientWrapperAsync.Object, mockCountriesHttpClientWrapperAsync.Object, mockCountriesTimeZoneHttpClientWrapperAsync.Object, mockUrlService.Object);
            var result = await countryService.GetCountryTimeZoneDictionary();
            Assert.AreEqual(result.Count, TimeZones.DistinctBy(tz => tz.CountryCode).Count());
        }
    }
}
