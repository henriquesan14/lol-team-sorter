using LoLTeamSorter.Application.Contracts.Services.Response;
using Refit;

namespace LoLTeamSorter.Infra.ExternalServices
{
    public interface IDiscordApi
    {
        [Post("/oauth2/token")]
        [Headers("Content-Type: application/x-www-form-urlencoded")]
        Task<DiscordTokenResponse> ObterToken([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> data);

        [Get("/users/@me")]
        Task<DiscordUserResponse> ObterUsuario([Header("Authorization")] string authorization);
    }
}
