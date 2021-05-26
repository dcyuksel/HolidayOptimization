using HolidayOptimization.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace HolidayOptimization.Application.Queries.CountriesWithMostUniqueHolidays
{
    public class CountriesWithMostUniqueHolidaysQuery : IRequest<Response<IEnumerable<CountriesWithMostUniqueHolidaysViewModel>>>
    {
        public int Year { get; set; }
    }
}
