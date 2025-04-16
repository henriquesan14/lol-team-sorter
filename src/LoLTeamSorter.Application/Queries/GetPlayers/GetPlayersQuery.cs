using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.ViewModels;
using MediatR;

namespace LoLTeamSorter.Application.Queries.GetPlayers
{
    public record GetPlayersQuery : IQuery<IEnumerable<PlayerViewModel>>
    {
    }
}
