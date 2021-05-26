using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace HolidayOptimization.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "HolidayOptimization.WebApi",
                    Description = "This Api will be responsible for holiday optimization.",
                    Contact = new OpenApiContact
                    {
                        Name = "Deniz Can Yüksel",
                        Email = "dnzcnyksl@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/dcyuksel/"),
                    }
                });
            });
        }
    }
}
