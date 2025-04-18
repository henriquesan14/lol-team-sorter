using LoLTeamSorter.Application.Contracts.CQRS;

namespace LoLTeamSorter.Application.Commands.DeleteMatchmaking
{
    public record DeleteMatchmakingCommand(Guid Id) : ICommand
    {
    }
}
