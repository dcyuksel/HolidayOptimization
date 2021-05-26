using HolidayOptimization.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolidayOptimization.Application.Interfaces
{
    public interface IPublicHolidayServiceAsync
    {
        Task<IEnumerable<PublicHoliday>> GetPublicHolidays(string countryCode, int year);
        Task<IEnumerable<IEnumerable<PublicHoliday>>> GetPublicHolidaysOfCountries(IEnumerable<Country> countries, int year);
    }
}
