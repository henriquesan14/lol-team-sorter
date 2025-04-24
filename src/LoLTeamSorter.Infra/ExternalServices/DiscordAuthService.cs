using LoLTeamSorter.Application.Contracts.Services;
using LoLTeamSorter.Application.Contracts.Services.Response;
using Microsoft.Extensions.Configuration;

namespace LoLTeamSorter.Infra.ExternalServices
{
    public class DiscordAuthService(IDiscordApi discordApi, IConfiguration configuration) : IDiscordAuthService
    {

        public async Task<DiscordTokenResponse?> ObterTokenAsync(string code)
        {
            var data = new Dictionary<string, string>
            {
                ["client_id"] = configuration["Discord:ClientId"]!,
                ["client_secret"] = configuration["Discord:ClientSecret"]!,
                ["grant_type"] = "authorization_code",
                ["code"] = code,
                ["redirect_uri"] = configuration["Discord:RedirectUri"]!
            };

            return await discordApi.ObterToken(data);
        }

        public async Task<DiscordUserResponse?> ObterUsuarioAsync(string accessToken)
        {
            return await discordApi.ObterUsuario($"Bearer {accessToken}");
        }
    }
}
