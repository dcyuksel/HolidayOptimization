using HolidayOptimization.Application.Interfaces;
using HolidayOptimization.Application.Interfaces.Shared;
using HolidayOptimization.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolidayOptimization.Persistence.Services
{
    public class PublicHolidayServiceAsync : IPublicHolidayServiceAsync
    {
        private readonly IHttpClientWrapperAsync<IEnumerable<PublicHoliday>> httpClientWrapperAsync;
        private readonly IUrlService urlService;

        public PublicHolidayServiceAsync(
            IHttpClientWrapperAsync<IEnumerable<PublicHoliday>> httpClientWrapperAsync,
            IUrlService urlService)
        {
            this.httpClientWrapperAsync = httpClientWrapperAsync;
            this.urlService = urlService;
        }

        public async Task<IEnumerable<PublicHoliday>> GetPublicHolidays(string countryCode, int year)
        {
            return await httpClientWrapperAsync.GetAsync(urlService.GetPublicHolidaysUrl(countryCode, year));
        }

        public async Task<IEnumerable<IEnumerable<PublicHoliday>>> GetPublicHolidaysOfCountries(IEnumerable<Country> countries, int year)
        {
            var urls = countries.Select(c => urlService.GetPublicHolidaysUrl(c.CountryCode, year));
            var publicHolidays = await httpClientWrapperAsync.GetMultipleAsync(urls);

            return publicHolidays;
        }
    }
}
