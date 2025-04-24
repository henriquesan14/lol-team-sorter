using LoLTeamSorter.Application.Contracts.Services.Response;

namespace LoLTeamSorter.Application.Contracts.Services
{
    public interface IDiscordAuthService
    {
        Task<DiscordTokenResponse?> ObterTokenAsync(string code);
        Task<DiscordUserResponse?> ObterUsuarioAsync(string accessToken);
    }
}
