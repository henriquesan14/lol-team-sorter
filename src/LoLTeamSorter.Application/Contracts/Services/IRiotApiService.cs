using LoLTeamSorter.Application.Contracts.Response;

namespace LoLTeamSorter.Infra.ExternalServices
{
    public interface IRiotApiService
    {
        Task<RiotAccountDto> GetAccountByRiotIdAsync(string gameName, string tagLine);
        Task<List<RiotLeagueEntryDto>> GetLeagueByRiotIdAsync(string riotId);
    }
}
