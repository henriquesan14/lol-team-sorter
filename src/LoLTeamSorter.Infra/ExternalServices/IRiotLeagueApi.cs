using LoLTeamSorter.Application.Contracts.Response;
using Refit;

namespace LoLTeamSorter.Infra.ExternalServices
{
    public interface IRiotLeagueApi
    {
        [Get("/lol/league/v4/entries/by-puuid/{puuid}")]
        Task<List<RiotLeagueEntryDto>> GetLeagueEntriesByPuuidAsync(string puuid);
    }
}
