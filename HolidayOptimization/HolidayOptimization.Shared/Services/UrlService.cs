using HolidayOptimization.Application.Interfaces.Shared;
using System.Text;

namespace HolidayOptimization.Shared.Services
{
    public class UrlService : IUrlService
    {
        private static readonly string AvailableCountriesUrl = "https://date.nager.at/api/v3/AvailableCountries";
        private static readonly string CountryInfoUrl = "https://date.nager.at/api/v3/CountryInfo/";
        private static readonly string PublicHolidaysUrl = "https://date.nager.at/api/v3/PublicHolidays/";
        private static readonly string TimeZonesUrl = "https://restcountries.eu/rest/v2/all";

        public string GetAvailableCountriesUrl()
        {
            return AvailableCountriesUrl;
        }

        public string GetCountryInfoUrl(string countryCode)
        {
            var stringBuilder = new StringBuilder(CountryInfoUrl);
            stringBuilder.Append(countryCode);

            return stringBuilder.ToString();
        }

        public string GetPublicHolidaysUrl(string countryCode, int year)
        {
            var stringBuilder = new StringBuilder(PublicHolidaysUrl);
            stringBuilder.Append(year);
            stringBuilder.Append("/");
            stringBuilder.Append(countryCode);

            return stringBuilder.ToString();
        }

        public string GetTimeZonesUrl()
        {
            return TimeZonesUrl;
        }
    }
}
