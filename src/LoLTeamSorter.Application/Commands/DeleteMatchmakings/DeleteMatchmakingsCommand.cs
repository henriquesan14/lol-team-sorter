using LoLTeamSorter.Application.Contracts.CQRS;

namespace LoLTeamSorter.Application.Commands.DeleteMatchmakings
{
    public record DeleteMatchmakingsCommand(List<Guid> MatchmakingIds) : ICommand
    {
    }
}
