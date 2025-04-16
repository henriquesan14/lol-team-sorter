using Carter;
using LoLTeamSorte.API.Extensions;
using LoLTeamSorter.Infra.ErrorHandling;

namespace LoLTeamSorte.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCarter();
            services.AddJsonSerializationConfig();
            services.AddCorsConfig();

            services.AddExceptionHandler<CustomExceptionHandler>();

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.MapCarter();

            app.UseExceptionHandler(options => { });

            return app;
        }
    }
}
