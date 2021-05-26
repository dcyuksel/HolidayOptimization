using HolidayOptimization.Application.Interfaces.Shared;
using HolidayOptimization.Application.Models;
using HolidayOptimization.Application.Queries.CountriesWithMostHoliday;
using HolidayOptimization.Application.Queries.CountriesWithMostUniqueHolidays;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolidayOptimization.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : BaseApiController
    {
        private readonly ICurrentDateTimeService currentDateTimeService;
        private readonly int ThisYear;

        public CountryController(ICurrentDateTimeService dateTimeService)
        {
            this.currentDateTimeService = dateTimeService;
            ThisYear = dateTimeService.GetCurrentYear();
        }

        [HttpGet("CountriesWithMostHolidaysThisYear")]
        [ProducesResponseType(typeof(Response<IEnumerable<CountriesWithMostHolidayViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountriesWithMostHolidayThisYear()
        {
            var query = new CountriesWithMostHolidayQuery { Year = ThisYear };

            return Ok(await Mediator.Send(query));
        }

        [HttpGet("CountriesWithMostHolidays")]
        [ProducesResponseType(typeof(Response<IEnumerable<CountriesWithMostHolidayViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountriesWithMostHoliday([FromQuery] CountriesWithMostHolidayQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("CountriesWithMostUniqueHolidaysThisYear")]
        [ProducesResponseType(typeof(Response<IEnumerable<CountriesWithMostUniqueHolidaysViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountriesWithMostUniqueHolidaysThisYear()
        {
            var query = new CountriesWithMostUniqueHolidaysQuery { Year = ThisYear };

            return Ok(await Mediator.Send(query));
        }

        [HttpGet("CountriesWithMostUniqueHolidays")]
        [ProducesResponseType(typeof(Response<IEnumerable<CountriesWithMostUniqueHolidaysViewModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountriesWithMostUniqueHolidays([FromQuery] CountriesWithMostUniqueHolidaysQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
