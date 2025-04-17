using LoLTeamSorter.Application.Contracts.Response;
using LoLTeamSorter.Application.Exceptions;
using Refit;
using System.Net;

namespace LoLTeamSorter.Infra.ExternalServices
{
    public class RiotApiService(IRiotAccountApi _riotAccountApi, IRiotLeagueApi _riotLeagueApi) : IRiotApiService
    {
        public async Task<RiotAccountDto> GetAccountByRiotIdAsync(string gameName, string tagLine)
        {
            try
            {
                return await _riotAccountApi.GetAccountByRiotIdAsync(gameName, tagLine);
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("Conta Riot não encontrada.");
            }
        }

        public async Task<List<RiotLeagueEntryDto>> GetLeagueByRiotIdAsync(string riotId)
        {
            try
            {
                return await _riotLeagueApi.GetLeagueEntriesByPuuidAsync(riotId);
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("Informações de liga não encontradas.");
            }
        }
    }
}
