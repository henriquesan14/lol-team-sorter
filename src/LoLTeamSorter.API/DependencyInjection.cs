using Carter;
using HealthChecks.UI.Client;
using LoLTeamSorter.API.Extensions;
using LoLTeamSorter.Infra.ErrorHandling;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Scalar.AspNetCore;

namespace LoLTeamSorter.API
{
    public static class DependencyInjection
    {
        public static void ConfigureHostUrls(this WebApplicationBuilder builder)
        {
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            builder.WebHost.UseUrls($"http://*:{port}");
        }

        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCarter();
            services.AddJsonSerializationConfig();
            services.AddCorsConfig();
            services.AddAuthConfig(configuration);

            services.AddExceptionHandler<CustomExceptionHandler>();

            services.AddOpenApi();

            services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("DbConnection")!);

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


            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app;
        }
    }
}
