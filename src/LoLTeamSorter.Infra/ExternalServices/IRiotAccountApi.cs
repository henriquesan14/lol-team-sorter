using LoLTeamSorter.Application.Contracts.Response;
using Refit;

namespace LoLTeamSorter.Infra.ExternalServices
{
    public interface IRiotAccountApi
    {
        [Get("/riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}")]
        Task<RiotAccountDto> GetAccountByRiotIdAsync(string gameName, string tagLine);
    }
}
