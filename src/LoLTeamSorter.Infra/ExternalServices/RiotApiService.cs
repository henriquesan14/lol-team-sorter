using LoLTeamSorter.Application.Contracts.Response;
using LoLTeamSorter.Application.Contracts.Services.Response;
using LoLTeamSorter.Application.Exceptions;
using Microsoft.Extensions.Configuration;
using Refit;
using System.Net;

namespace LoLTeamSorter.Infra.ExternalServices
{
    public class RiotApiService(IConfiguration configuration, IRiotAccountApi riotAccountApi, IRiotLeagueApi riotLeagueApi, IChampionMasteryApi championMasteryApi,
        IDDragonApi ddragonApi) : IRiotApiService
    {
        public async Task<RiotAccountDto> GetAccountByRiotIdAsync(string gameName, string tagLine)
        {
            try
            {
                return await riotAccountApi.GetAccountByRiotIdAsync(gameName, tagLine);
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
                return await riotLeagueApi.GetLeagueEntriesByPuuidAsync(riotId);
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("Informações de liga não encontradas.");
            }
        }

        public async Task<List<ChampionMasteryDto>> GetChampionMasteriesByRiotIdAsyncs(string riotId)
        {
            var topMasteries = await championMasteryApi.GetTopChampionsMasteryByPuuidAsync(riotId, 5);
            var championMap = await GetChampionMapAsync();

            foreach (var mastery in topMasteries)
            {
                if (championMap.TryGetValue(mastery.ChampionId, out var champData))
                {
                    mastery.Name = champData.Nome;
                    mastery.ImageUrl = champData.ImagemUrl;
                }
            }

            return topMasteries;
        }

        private async Task<Dictionary<int, ChampionInfoDto>> GetChampionMapAsync()
        {
            var championData = await ddragonApi.GetChampionDataAsync();

            var championMap = championData.Data.Values
                .ToDictionary(
                    x => int.Parse(x.Key),
                    x => new ChampionInfoDto(x.Name, $"{configuration["RiotApi:DDragonUrlBase"]}/cdn/14.8.1/img/champion/{x.Image.Full}"));

            return championMap;
        }
    }
}
