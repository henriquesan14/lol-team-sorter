using LoLTeamSorter.Application.Contracts.Response;
using LoLTeamSorter.Application.Contracts.Services.Response;
using LoLTeamSorter.Application.Exceptions;
using Microsoft.Extensions.Configuration;
using Refit;
using System.Net;

namespace LoLTeamSorter.Infra.ExternalServices
{
    public class RiotApiService(IConfiguration configuration, IRiotAccountApi riotAccountApi, IRiotLeagueApi riotLeagueApi, IChampionMasteryApi championMasteryApi,
        IDDragonApi ddragonApi, IRiotMatchApi riotMatchApi, IDDragonVersionApi dDragonVersionApi) : IRiotApiService
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

        public async Task<List<ChampionRankedStatsDto>> GetRankedChampionStatsAsync(string riotId)
        {
            var versions = await dDragonVersionApi.GetVersionsAsync();
            var latestVersion = versions.FirstOrDefault();

            var matchIds = await riotMatchApi.GetMatchIdsAsync(riotId, count: 25);
            var championStats = new Dictionary<int, ChampionRankedStatsDto>();

            var matchTasks = new List<Task<MatchDto>>();

            foreach (var matchId in matchIds)
            {
                matchTasks.Add(riotMatchApi.GetMatchAsync(matchId));
                await Task.Delay(100);
            }

            var matches = await Task.WhenAll(matchTasks);

            foreach (var match in matches)
            {
                if (match.Info.GameDuration < 300)
                    continue;

                var player = match.Info.Participants.FirstOrDefault(p => p.Puuid == riotId);
                if (player == null)
                    continue;

                if (!championStats.TryGetValue(player.ChampionId, out var stats))
                {
                    stats = new ChampionRankedStatsDto(player.ChampionId)
                    {
                        Name = player.ChampionName,
                        ImageUrl = $"{configuration["RiotApi:DDragonUrlBase"]}/cdn/{latestVersion}/img/champion/{player.ChampionName}.png"
                    };
                    championStats[player.ChampionId] = stats;
                }

                stats.Matches++;
                if (player.Win)
                    stats.Wins++;
                else
                    stats.Losses++;

                stats.TotalKills += player.Kills;
                stats.TotalDeaths += player.Deaths;
                stats.TotalAssists += player.Assists;
            }

            return championStats.Values
                .OrderByDescending(x => x.Matches)
                .ToList();
        }

        private async Task<Dictionary<int, ChampionInfoDto>> GetChampionMapAsync()
        {
            var versions = await dDragonVersionApi.GetVersionsAsync();
            var latestVersion = versions.FirstOrDefault();
            var championData = await ddragonApi.GetChampionDataAsync(latestVersion!);

            var championMap = championData.Data.Values
                .ToDictionary(
                    x => int.Parse(x.Key),
                    x => new ChampionInfoDto(x.Name, $"{configuration["RiotApi:DDragonUrlBase"]}/cdn/{latestVersion}/img/champion/{x.Image.Full}"));

            return championMap;
        }
    }
}
