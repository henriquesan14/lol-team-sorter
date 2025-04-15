using LoLTeamSorter.Application.Contracts.Response;

namespace LoLTeamSorter.Infra.ExternalServices
{
    public class RiotApiService(IRiotAccountApi _riotAccountApi, IRiotLeagueApi _riotLeagueApi) : IRiotApiService
    {
        public async Task<RiotAccountDto> GetAccountByRiotIdAsync(string gameName, string tagLine)
        {
            return await _riotAccountApi.GetAccountByRiotIdAsync(gameName, tagLine);
        }

        public async Task<List<RiotLeagueEntryDto>> GetLeagueByRiotIdAsync(string riotId)
        {
            return await _riotLeagueApi.GetLeagueEntriesByPuuidAsync(riotId);
        }
    }
}
