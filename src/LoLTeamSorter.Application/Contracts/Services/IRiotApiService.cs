using LoLTeamSorter.Application.Contracts.Response;
using LoLTeamSorter.Application.Contracts.Services.Response;

namespace LoLTeamSorter.Infra.ExternalServices
{
    public interface IRiotApiService
    {
        Task<RiotAccountDto> GetAccountByRiotIdAsync(string gameName, string tagLine);
        Task<List<RiotLeagueEntryDto>> GetLeagueByRiotIdAsync(string riotId);
        Task<List<ChampionMasteryDto>> GetChampionMasteriesByRiotIdAsyncs(string riotId);
        Task<List<ChampionRankedStatsDto>> GetRankedChampionStatsAsync(string riotId);
    }
}
