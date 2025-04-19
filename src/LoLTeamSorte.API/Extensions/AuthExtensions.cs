using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LoLTeamSorte.API.Extensions
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
        {
            const string claimType = "Permissoes";
            var secretKey = Encoding.ASCII.GetBytes(configuration["TokenSettings:Secret"]!);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
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
            });

            var permissoes = new Dictionary<string, string[]>
            {
                // Processo
                ["CadastrarPlayer"] = ["CADASTRAR_PLAYER"],
                ["EditarPlayer"] = ["EDITAR_PROCESSO"],
                ["ExcluirPlayer"] = ["EXCLUIR_PROCESSO"],
                ["VisualizarPlayer"] = ["LISTAR_PROCESSO"],

                //Sorteio
                ["VisualizarSorteio"] = ["VISUALIZAR_SORTEIO"],
                ["GerarSorteio"] = ["GERAR_SORTEIO"],
                ["ExcluirSorteio"] = ["EXCLUIR_SORTEIO"]
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
