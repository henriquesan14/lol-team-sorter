using Carter;
using LoLTeamSorter.API.Extensions;
using LoLTeamSorter.Infra.ErrorHandling;
using Scalar.AspNetCore;

namespace LoLTeamSorter.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCarter();
            services.AddJsonSerializationConfig();
            services.AddCorsConfig();
            services.AddAuthConfig(configuration);

            services.AddExceptionHandler<CustomExceptionHandler>();

            services.AddOpenApi();


            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.MapCarter();

            app.UseExceptionHandler(options => { });

            app.UseCors("AllowSpecificOrigin");

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
