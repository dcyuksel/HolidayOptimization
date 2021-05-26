using HolidayOptimization.Application.Interfaces;
using HolidayOptimization.Application.Interfaces.Shared;
using HolidayOptimization.Domain.Entities;
using HolidayOptimization.Persistence.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolidayOptimization.Persistence.Services
{
    public class CountryServiceAsync : ICountryServiceAsync
    {
        private readonly IHttpClientWrapperAsync<CountryInformation> countryInformationHttpClientWrapperAsync;
        private readonly IHttpClientWrapperAsync<IEnumerable<Country>> countriesHttpClientWrapperAsync;
        private readonly IHttpClientWrapperAsync<IEnumerable<CountryTimeZone>> countriesTimeZoneHttpClientWrapperAsync;
        private readonly IUrlService urlService;

        public CountryServiceAsync(
            IHttpClientWrapperAsync<CountryInformation> countryInformationHttpClientWrapperAsync,
            IHttpClientWrapperAsync<IEnumerable<Country>> countriesHttpClientWrapperAsync,
            IHttpClientWrapperAsync<IEnumerable<CountryTimeZone>> countriesTimeZoneHttpClientWrapperAsync,
            IUrlService urlService)
        {
            this.countryInformationHttpClientWrapperAsync = countryInformationHttpClientWrapperAsync;
            this.countriesHttpClientWrapperAsync = countriesHttpClientWrapperAsync;
            this.countriesTimeZoneHttpClientWrapperAsync = countriesTimeZoneHttpClientWrapperAsync;
            this.urlService = urlService;
        }

        public async Task<CountryInformation> GetCountryInformationAsync(string countryCode)
        {
            return await countryInformationHttpClientWrapperAsync.GetAsync(urlService.GetCountryInfoUrl(countryCode));
        }

        public async Task<IEnumerable<Country>> GetAvailableCountriesAsync()
        {
            return await countriesHttpClientWrapperAsync.GetAsync(urlService.GetAvailableCountriesUrl());
        }

        public async Task<Dictionary<string, IEnumerable<string>>> GetCountryTimeZoneDictionary()
        {
            var url = urlService.GetTimeZonesUrl();
            var countryTimeZones = await countriesTimeZoneHttpClientWrapperAsync.GetAsync(url);
            var countryTimeZonesDictionary = countryTimeZones.ToCountryTimeZoneDictionary();

            return countryTimeZonesDictionary;
        }
    }
}
