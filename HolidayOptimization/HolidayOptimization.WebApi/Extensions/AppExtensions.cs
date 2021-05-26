using HolidayOptimization.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace HolidayOptimization.WebApi.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HolidayOptimization.WebApi");
                ///c.RoutePrefix = string.Empty;
            });
        }

        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
