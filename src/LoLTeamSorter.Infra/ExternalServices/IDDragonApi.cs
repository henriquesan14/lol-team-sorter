using LoLTeamSorter.Application.Contracts.Services.Response;
using Refit;

namespace LoLTeamSorter.Infra.ExternalServices
{
    public interface IDDragonApi
    {
        [Get("/cdn/{version}/data/en_US/champion.json")]
        Task<ChampionDataDto> GetChampionDataAsync(string version);
    }
}
