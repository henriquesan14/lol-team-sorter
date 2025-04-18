using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Services.Response;

namespace LoLTeamSorter.Application.Queries.GetChampionRankedStats
{
    public record GetChampionRankedStatsQuery(string riotId) : IQuery<List<ChampionRankedStatsDto>>
    {
    }
}
