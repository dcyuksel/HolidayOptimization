using HolidayOptimization.Application.Interfaces;
using HolidayOptimization.Application.Models;
using HolidayOptimization.Application.Queries.MonthsWithMostHoliday.Extensions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HolidayOptimization.Application.Queries.MonthsWithMostHoliday
{
    public class MonthsWithMostHolidayQueryHandler : IRequestHandler<MonthsWithMostHolidayQuery, Response<IEnumerable<int>>>
    {
        private readonly ICountryServiceAsync countryServiceAsync;
        private readonly IPublicHolidayServiceAsync publicHolidayServiceAsync;

        public MonthsWithMostHolidayQueryHandler(
            ICountryServiceAsync countryServiceAsync,
            IPublicHolidayServiceAsync publicHolidayServiceAsync)
        {
            this.countryServiceAsync = countryServiceAsync;
            this.publicHolidayServiceAsync = publicHolidayServiceAsync;
        }

        public async Task<Response<IEnumerable<int>>> Handle(MonthsWithMostHolidayQuery query, CancellationToken cancellationToken)
        {
            var availableCountries = await countryServiceAsync.GetAvailableCountriesAsync();
            var countriesPublicHolidays = await publicHolidayServiceAsync.GetPublicHolidaysOfCountries(availableCountries, query.Year);
            var monthsWithMostHolidays = countriesPublicHolidays.GetMonthsWithMostHolidays();

            return new Response<IEnumerable<int>>(monthsWithMostHolidays);
        }
    }
}
