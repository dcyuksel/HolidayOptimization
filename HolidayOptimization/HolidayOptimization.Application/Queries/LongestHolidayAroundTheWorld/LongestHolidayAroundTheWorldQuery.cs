using HolidayOptimization.Application.Models;
using MediatR;

namespace HolidayOptimization.Application.Queries.LongestHolidayAroundTheWorld
{
    public class LongestHolidayAroundTheWorldQuery : IRequest<Response<LongestHolidayAroundTheWorldViewModel>>
    {
        public int Year { get; set; }
    }
}
