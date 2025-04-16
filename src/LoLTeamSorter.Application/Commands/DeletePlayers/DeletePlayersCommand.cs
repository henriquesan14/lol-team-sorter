using LoLTeamSorter.Application.Contracts.CQRS;

namespace LoLTeamSorter.Application.Commands.DeletePlayers
{
    public record DeletePlayersCommand(List<Guid> PlayerIds) : ICommand
    {
    }
}
