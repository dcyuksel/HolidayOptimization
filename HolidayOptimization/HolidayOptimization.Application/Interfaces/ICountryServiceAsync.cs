using HolidayOptimization.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HolidayOptimization.Application.Interfaces
{
    public interface ICountryServiceAsync
    {
        Task<CountryInformation> GetCountryInformationAsync(string countryCode);
        Task<IEnumerable<Country>> GetAvailableCountriesAsync();
        Task<Dictionary<string, IEnumerable<string>>> GetCountryTimeZoneDictionary();
    }
}
