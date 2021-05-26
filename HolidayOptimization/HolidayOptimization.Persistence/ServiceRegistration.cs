using HolidayOptimization.Application.Interfaces;
using HolidayOptimization.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HolidayOptimization.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddTransient<ICountryServiceAsync, CountryServiceAsync>();
            services.AddTransient<IPublicHolidayServiceAsync, PublicHolidayServiceAsync>();
        }
    }
}
