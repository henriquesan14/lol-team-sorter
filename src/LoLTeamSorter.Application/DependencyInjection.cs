﻿using LoLTeamSorter.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LoLTeamSorter.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
            });

            services.AddScoped<MatchmakingService>();

            return services;
        }
    }
}
