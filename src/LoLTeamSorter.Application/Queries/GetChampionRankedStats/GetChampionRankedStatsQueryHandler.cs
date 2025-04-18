using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Services.Response;
using LoLTeamSorter.Infra.ExternalServices;

namespace LoLTeamSorter.Application.Queries.GetChampionRankedStats
{
    public class GetChampionRankedStatsQueryHandler(IRiotApiService riotApiService) : IQueryHandler<GetChampionRankedStatsQuery, List<ChampionRankedStatsDto>>
    {
        public async Task<List<ChampionRankedStatsDto>> Handle(GetChampionRankedStatsQuery request, CancellationToken cancellationToken)
        {
            return await riotApiService.GetRankedChampionStatsAsync(request.riotId);
        }
    }
}
