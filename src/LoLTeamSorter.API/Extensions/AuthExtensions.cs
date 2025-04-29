using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LoLTeamSorter.API.Extensions
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            var isDevelopment = env.IsDevelopment();

            const string claimType = "Permissions";
            var secretKey = Encoding.ASCII.GetBytes(configuration["TokenSettings:Secret"]!);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = !isDevelopment;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true
                };

                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Cookies["access_token"];
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });



            var permissoes = new Dictionary<string, string[]>
            {
                // Player
                ["CreatePlayer"] = ["CREATE_PLAYER"],
                ["EditPlayer"] = ["EDIT_PLAYER"],
                ["DeletePlayer"] = ["DELETE_PLAYER"],
                ["ViewPlayer"] = ["VIEW_PLAYER"],
                ["UpdateRankedTierPlayer"] = ["UPDATE_RANKED_TIER_PLAYER"],

                //Matchmaking
                ["ViewMatchmaking"] = ["VIEW_MATCHMAKING"],
                ["GenerateMatchmaking"] = ["GENERATE_MATCHMAKING"],
                ["DeleteMatchmaking"] = ["DELETE_MATCHMAKING"],

                // User
                ["ViewUser"] = ["VIEW_USER"],
                ["CreateUser"] = ["CREATE_USER"],
                ["EditUser"] = ["EDIT_USER"],
                ["DeleteUser"] = ["DELETE_USER"]
            };

            var builder = services.AddAuthorizationBuilder();
            foreach (var (policyName, requiredClaims) in permissoes)
            {
                builder.AddPolicy(policyName, policy =>
                    policy.RequireClaim(claimType, requiredClaims));
            }

            return services;
        }
    }
}
