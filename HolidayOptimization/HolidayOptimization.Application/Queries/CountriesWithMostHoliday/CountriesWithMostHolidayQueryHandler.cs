using HolidayOptimization.Application.Interfaces;
using HolidayOptimization.Application.Models;
using HolidayOptimization.Application.Queries.CountriesWithMostHoliday.Extensions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HolidayOptimization.Application.Queries.CountriesWithMostHoliday
{
    public class CountriesWithMostHolidayQueryHandler : IRequestHandler<CountriesWithMostHolidayQuery, Response<IEnumerable<CountriesWithMostHolidayViewModel>>>
    {
        private readonly ICountryServiceAsync countryServiceAsync;
        private readonly IPublicHolidayServiceAsync publicHolidayServiceAsync;

        public CountriesWithMostHolidayQueryHandler(
            ICountryServiceAsync countryServiceAsync,
            IPublicHolidayServiceAsync publicHolidayServiceAsync)
        {
            this.countryServiceAsync = countryServiceAsync;
            this.publicHolidayServiceAsync = publicHolidayServiceAsync;
        }

        public async Task<Response<IEnumerable<CountriesWithMostHolidayViewModel>>> Handle(CountriesWithMostHolidayQuery query, CancellationToken cancellationToken)
        {
            var availableCountries = await countryServiceAsync.GetAvailableCountriesAsync();
            var countriesPublicHolidays = await publicHolidayServiceAsync.GetPublicHolidaysOfCountries(availableCountries, query.Year);
            var countriesWithMostHoliday = countriesPublicHolidays.GetCountriesWithMostHolidays();
            var vieModels = await ToViewModel(countriesWithMostHoliday);

            return new Response<IEnumerable<CountriesWithMostHolidayViewModel>>(vieModels);
        }

        private async Task<IEnumerable<CountriesWithMostHolidayViewModel>> ToViewModel(IEnumerable<string> countryCodes)
        {
            // Usually there is only one country, there is no need for parallelization
            var viewModels = new List<CountriesWithMostHolidayViewModel>();
            foreach(var countryCode in countryCodes)
            {
                var countryInformation = await countryServiceAsync.GetCountryInformationAsync(countryCode);
                var viewModel = new CountriesWithMostHolidayViewModel 
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
