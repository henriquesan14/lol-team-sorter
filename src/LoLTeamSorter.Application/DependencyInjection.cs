using FluentValidation;
using LoLTeamSorter.Application.Behaviors;
using LoLTeamSorter.Application.Services;
using LoLTeamSorter.Application.Validators;
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
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssemblyContaining<CreatePlayerCommandValidator>();

            services.AddScoped<MatchmakingService>();

            return services;
        }
    }
}
