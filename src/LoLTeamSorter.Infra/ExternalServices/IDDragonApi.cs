using LoLTeamSorter.Application.Contracts.Services.Response;
using Refit;

namespace LoLTeamSorter.Infra.ExternalServices
{
    public interface IDDragonApi
    {
        [Get("/cdn/14.8.1/data/en_US/champion.json")]
        Task<ChampionDataDto> GetChampionDataAsync();
    }
}
