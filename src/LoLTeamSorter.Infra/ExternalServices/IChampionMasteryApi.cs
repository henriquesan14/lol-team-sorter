using LoLTeamSorter.Application.Contracts.Services.Response;
using Refit;

namespace LoLTeamSorter.Infra.ExternalServices
{
    public interface IChampionMasteryApi
    {
        [Get("/lol/champion-mastery/v4/champion-masteries/by-puuid/{puuid}/top")]
        Task<List<ChampionMasteryDto>> GetTopChampionsMasteryByPuuidAsync(string puuid, [Query] int count);
    }
}
