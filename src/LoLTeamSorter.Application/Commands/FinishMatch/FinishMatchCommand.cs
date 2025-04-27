using LoLTeamSorter.Application.Contracts.CQRS;

namespace LoLTeamSorter.Application.Commands.FinishMatch
{
    public record FinishMatchCommand(Guid MatchmakingId, Guid WinningTeamId) : ICommand;
}
