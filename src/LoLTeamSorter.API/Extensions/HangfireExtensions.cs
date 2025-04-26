using Hangfire.Dashboard.BasicAuthorization;
using Hangfire;
using Hangfire.PostgreSql;
using LoLTeamSorter.Application.Contracts.Services;

namespace LoLTeamSorter.API.Extensions
{
    public static class HangfireExtensions
    {
        public static IServiceCollection AddHangfireConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");

            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(options => options.UseNpgsqlConnection(connectionString)));

            services.AddHangfireServer();

            return services;
        }


        public static IApplicationBuilder UseHangfireDashboardWithAuth(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[]
                {
                    new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                    {
                        RequireSsl = false,
                        SslRedirect = false,
                        LoginCaseSensitive = true,
                        Users = new[]
                        {
                            new BasicAuthAuthorizationUser
                            {
                                Login = configuration["Hangfire:Login"],
                                PasswordClear = configuration["Hangfire:Password"]
                            }
                        }
                    })
                }
            });

            return app;
        }

        public static IApplicationBuilder UseRecurringJobs(this IApplicationBuilder app)
        {
            RecurringJob.AddOrUpdate<ITokenCleanupService>(
                "CheckUpcomingEvents",
                service => service.CleanupExpiredAndRevokedTokensAsync(),
                "55 11 * * *");

            return app;
        }
    }
}
