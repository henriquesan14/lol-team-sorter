using Carter;
using LoLTeamSorte.API.Extensions;

namespace LoLTeamSorte.API
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCarter();
            services.AddJsonSerializationConfig();

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.MapCarter();

            return app;
        }
    }
}
