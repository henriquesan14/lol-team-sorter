using LoLTeamSorter.Application.ViewModels;
using MediatR;

namespace LoLTeamSorter.Application.Queries.GetPlayers
{
    public record GetPlayersQuery : IRequest<IEnumerable<PlayerViewModel>>
    {
    }
}
