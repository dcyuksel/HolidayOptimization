namespace HolidayOptimization.Application.Interfaces.Shared
{
    public interface IUrlService
    {
        string GetAvailableCountriesUrl();
        string GetCountryInfoUrl(string countryCode);
        string GetPublicHolidaysUrl(string countryCode, int year);
        string GetTimeZonesUrl();
    }
}
