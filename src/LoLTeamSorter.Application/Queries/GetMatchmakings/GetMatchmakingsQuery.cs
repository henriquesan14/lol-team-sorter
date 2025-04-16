using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.ViewModels;
using MediatR;

namespace LoLTeamSorter.Application.Queries.GetMatchmakings
{
    public record GetMatchmakingsQuery : IQuery<IEnumerable<MatchmakingViewModel>>
    {
    }
}
