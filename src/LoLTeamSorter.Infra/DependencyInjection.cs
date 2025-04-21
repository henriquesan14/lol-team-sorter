using LoLTeamSorter.Contracts.Data;
using LoLTeamSorter.Infra.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using LoLTeamSorter.Infra.Data.Interceptors;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Infra.Data.Repositories;
using LoLTeamSorter.Infra.ExternalServices;
using Refit;
using LoLTeamSorter.Application.Contracts.Services;
using LoLTeamSorter.Infra.Services;

namespace LoLTeamSorter.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure
            (this IServiceCollection services, IConfiguration configuration)
        {
            

            var connectionString = configuration.GetConnectionString("DbConnection");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

            services.AddDbContext<LoLTeamSorterDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<ILoLTeamSorterDbContext, LoLTeamSorterDbContext>();

            //Repositories
            services.AddScoped(typeof(IAsyncRepository<,>), typeof(RepositoryBase<,>));
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IMatchmakingRepository, MatchmakingRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Services
            services.AddRefitClient<IRiotAccountApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(configuration["RiotApi:AmericasUrlBase"]!);
                    c.DefaultRequestHeaders.Add("X-Riot-Token", configuration["RiotApi:ApiKey"]!);
                });

            services.AddRefitClient<IRiotLeagueApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(configuration["RiotApi:Br1UrlBase"]!);
                    c.DefaultRequestHeaders.Add("X-Riot-Token", configuration["RiotApi:ApiKey"]!);
                });

            services.AddRefitClient<IChampionMasteryApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(configuration["RiotApi:Br1UrlBase"]!);
                    c.DefaultRequestHeaders.Add("X-Riot-Token", configuration["RiotApi:ApiKey"]!);
                });

            services.AddRefitClient<IDDragonApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(configuration["RiotApi:DDragonUrlBase"]!);
                });

            services.AddRefitClient<IDDragonVersionApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(configuration["RiotApi:DDragonUrlBase"]!);
                });

            services.AddRefitClient<IRiotMatchApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(configuration["RiotApi:AmericasUrlBase"]!);
                    c.DefaultRequestHeaders.Add("X-Riot-Token", configuration["RiotApi:ApiKey"]!);
                });



            services.AddScoped<IRiotApiService, RiotApiService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
