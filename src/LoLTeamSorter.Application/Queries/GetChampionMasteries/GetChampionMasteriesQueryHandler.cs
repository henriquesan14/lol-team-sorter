using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Services.Response;
using LoLTeamSorter.Infra.ExternalServices;

namespace LoLTeamSorter.Application.Queries.GetChampionMasteries
{
    public class GetChampionMasteriesQueryHandler(IRiotApiService riotApiService) : IQueryHandler<GetChampionMasteriesQuery, List<ChampionMasteryDto>>
    {
        public Task<List<ChampionMasteryDto>> Handle(GetChampionMasteriesQuery request, CancellationToken cancellationToken)
        {
            return riotApiService.GetChampionMasteriesByRiotIdAsyncs(request.riotId);
        }
    }
}
