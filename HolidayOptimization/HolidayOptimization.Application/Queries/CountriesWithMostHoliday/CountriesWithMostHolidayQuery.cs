using HolidayOptimization.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace HolidayOptimization.Application.Queries.CountriesWithMostHoliday
{
    public class CountriesWithMostHolidayQuery : IRequest<Response<IEnumerable<CountriesWithMostHolidayViewModel>>>
    {
        public int Year { get; set; }
    }
}
