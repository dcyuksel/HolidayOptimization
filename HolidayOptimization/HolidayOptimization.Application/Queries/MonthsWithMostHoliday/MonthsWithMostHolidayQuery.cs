using HolidayOptimization.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace HolidayOptimization.Application.Queries.MonthsWithMostHoliday
{
    public class MonthsWithMostHolidayQuery : IRequest<Response<IEnumerable<int>>>
    {
        public int Year { get; set; }
    }
}
