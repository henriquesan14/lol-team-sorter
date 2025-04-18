using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Services.Response;

namespace LoLTeamSorter.Application.Queries.GetChampionMasteries
{
    public record GetChampionMasteriesQuery(string riotId) : IQuery<List<ChampionMasteryDto>>
    {
    }
}
