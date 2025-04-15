using LoLTeamSorter.Application.ViewModels;
using MediatR;

namespace LoLTeamSorter.Application.Queries.GetMatchmakings
{
    public record GetMatchmakingsQuery : IRequest<IEnumerable<MatchmakingViewModel>>
    {
    }
}
