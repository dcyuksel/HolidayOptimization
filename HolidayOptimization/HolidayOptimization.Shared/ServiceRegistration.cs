using HolidayOptimization.Application.Interfaces.Shared;
using HolidayOptimization.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HolidayOptimization.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedServices(this IServiceCollection services)
        {
            services.AddTransient<IUrlService, UrlService>();
            services.AddTransient<IHttpClientServiceAsync, HttpClientServiceAsync>();
            services.AddTransient<ICurrentDateTimeService, CurrentDateTimeService>();
            services.AddTransient(typeof(IHttpClientWrapperAsync<>), typeof(HttpClientWrapperAsync<>));
        }
    }
}
