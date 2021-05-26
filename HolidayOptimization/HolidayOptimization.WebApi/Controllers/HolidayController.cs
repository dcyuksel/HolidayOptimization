using HolidayOptimization.Application.Interfaces.Shared;
using HolidayOptimization.Application.Models;
using HolidayOptimization.Application.Queries.LongestHolidayAroundTheWorld;
using HolidayOptimization.Application.Queries.MonthsWithMostHoliday;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolidayOptimization.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : BaseApiController
    {
        private readonly ICurrentDateTimeService currentDateTimeService;
        private readonly int ThisYear;

        public HolidayController(ICurrentDateTimeService dateTimeService)
        {
            this.currentDateTimeService = dateTimeService;
            ThisYear = dateTimeService.GetCurrentYear();
        }

        [HttpGet("MonthsWithMostHolidaysThisYear")]
        [ProducesResponseType(typeof(Response<IEnumerable<int>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMonthsWithMostHolidaysThisYear()
        {
            var query = new MonthsWithMostHolidayQuery { Year = ThisYear };

            return Ok(await Mediator.Send(query));
        }

        [HttpGet("MonthsWithMostHolidays")]
        [ProducesResponseType(typeof(Response<IEnumerable<int>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMonthsWithMostHolidays([FromQuery] MonthsWithMostHolidayQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("LongestHolidayAroundTheWorldThisYear")]
        [ProducesResponseType(typeof(Response<LongestHolidayAroundTheWorldViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLongestHolidayAroundTheWorldThisYear()
        {
            var query = new LongestHolidayAroundTheWorldQuery { Year = ThisYear };

            return Ok(await Mediator.Send(query));
        }

        [HttpGet("LongestHolidayAroundTheWorld")]
        [ProducesResponseType(typeof(Response<LongestHolidayAroundTheWorldViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLongestHolidayAroundTheWorld([FromQuery] LongestHolidayAroundTheWorldQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
