using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.ViewModels;

namespace LoLTeamSorter.Application.Queries.GetPlayers
{
    public record GetPlayersQuery(string OrderBy = "Stars", bool Descending = true) : IQuery<IEnumerable<PlayerViewModel>>
    {
    }
}
