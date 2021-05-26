using HolidayOptimization.Application.Interfaces;
using HolidayOptimization.Application.Models;
using HolidayOptimization.Application.Queries.LongestHolidayAroundTheWorld.Extensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HolidayOptimization.Application.Queries.LongestHolidayAroundTheWorld
{
    public class LongestHolidayAroundTheWorldQueryHandler : IRequestHandler<LongestHolidayAroundTheWorldQuery, Response<LongestHolidayAroundTheWorldViewModel>>
    {
        private readonly ICountryServiceAsync countryServiceAsync;
        private readonly IPublicHolidayServiceAsync publicHolidayServiceAsync;

        public LongestHolidayAroundTheWorldQueryHandler(
            ICountryServiceAsync countryServiceAsync,
            IPublicHolidayServiceAsync publicHolidayServiceAsync)
        {
            this.countryServiceAsync = countryServiceAsync;
            this.publicHolidayServiceAsync = publicHolidayServiceAsync;
        }

        public async Task<Response<LongestHolidayAroundTheWorldViewModel>> Handle(LongestHolidayAroundTheWorldQuery query, CancellationToken cancellationToken)
        {
            var year = query.Year;
            var availableCountries = await countryServiceAsync.GetAvailableCountriesAsync();
            var countryTimeZonesDictionary = await countryServiceAsync.GetCountryTimeZoneDictionary();
            var countriesPublicHolidays = await publicHolidayServiceAsync.GetPublicHolidaysOfCountries(availableCountries, year);
            var longestHolidayAroundTheWorld = countriesPublicHolidays.GetLongestHolidayAroundTheWorld(countryTimeZonesDictionary, year);
            var viewModel = new LongestHolidayAroundTheWorldViewModel { StartTimeInUtc = longestHolidayAroundTheWorld.StartTime, EndTimeInUtc = longestHolidayAroundTheWorld.EndTime };

            return new Response<LongestHolidayAroundTheWorldViewModel>(viewModel);
        }
    }
}
