using LoLTeamSorter.Application.Contracts.CQRS;

namespace LoLTeamSorter.Application.Commands.DeletePlayer
{
    public record DeletePlayerCommand(Guid Id) : ICommand
    {
    }
}
