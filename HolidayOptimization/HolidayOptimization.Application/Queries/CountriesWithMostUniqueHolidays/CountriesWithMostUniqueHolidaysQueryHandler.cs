using HolidayOptimization.Application.Interfaces;
using HolidayOptimization.Application.Models;
using HolidayOptimization.Application.Queries.CountriesWithMostUniqueHolidays.Extensions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HolidayOptimization.Application.Queries.CountriesWithMostUniqueHolidays
{
    public class CountriesWithMostUniqueHolidaysQueryHandler : IRequestHandler<CountriesWithMostUniqueHolidaysQuery, Response<IEnumerable<CountriesWithMostUniqueHolidaysViewModel>>>
    {
        private readonly ICountryServiceAsync countryServiceAsync;
        private readonly IPublicHolidayServiceAsync publicHolidayServiceAsync;

        public CountriesWithMostUniqueHolidaysQueryHandler(
            ICountryServiceAsync countryServiceAsync,
            IPublicHolidayServiceAsync publicHolidayServiceAsync)
        {
            this.countryServiceAsync = countryServiceAsync;
            this.publicHolidayServiceAsync = publicHolidayServiceAsync;
        }

        public async Task<Response<IEnumerable<CountriesWithMostUniqueHolidaysViewModel>>> Handle(CountriesWithMostUniqueHolidaysQuery query, CancellationToken cancellationToken)
        {
            var availableCountries = await countryServiceAsync.GetAvailableCountriesAsync();
            var countriesPublicHolidays = await publicHolidayServiceAsync.GetPublicHolidaysOfCountries(availableCountries, query.Year);
            var countriesWithMostUniqueHolidays = countriesPublicHolidays.GetCountriesUniqueHoliday();
            var vieModels = await ToViewModel(countriesWithMostUniqueHolidays);

            return new Response<IEnumerable<CountriesWithMostUniqueHolidaysViewModel>>(vieModels);
        }

        private async Task<IEnumerable<CountriesWithMostUniqueHolidaysViewModel>> ToViewModel(IEnumerable<string> countryCodes)
        {
            // Usually there is only one country, there is no need for parallelization
            var viewModels = new List<CountriesWithMostUniqueHolidaysViewModel>();
            foreach (var countryCode in countryCodes)
            {
                var countryInformation = await countryServiceAsync.GetCountryInformationAsync(countryCode);
                var viewModel = new CountriesWithMostUniqueHolidaysViewModel
                {
                    CountryCode = countryCode,
                    CommonName = countryInformation.CommonName,
                    OfficialName = countryInformation.OfficialName
                };
                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
