using AspNetCoreRateLimit;
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
            if (builder.Environment.IsProduction())
            {
                var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
                builder.WebHost.UseUrls($"http://*:{port}");
            }
        }

        public static IServiceCollection AddApiServices(this IServiceCollection services, WebApplicationBuilder builder, IConfiguration configuration)
        {
            services.AddCorsConfig(builder.Environment);
            services.AddCarter();
            services.AddJsonSerializationConfig();
            services.AddAuthConfig(configuration);

            services.AddExceptionHandler<CustomExceptionHandler>();

            services.AddOpenApi();

            services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("DbConnection")!);

            services.AddHangfireConfig(configuration);

            services.AddRateLimitingConfig(builder.Configuration);

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app, IConfiguration configuration)
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

            app.UseIpRateLimiting();

            app.UseHangfireDashboardWithAuth(configuration);
            app.UseRecurringJobs();

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app;
        }
    }
}
