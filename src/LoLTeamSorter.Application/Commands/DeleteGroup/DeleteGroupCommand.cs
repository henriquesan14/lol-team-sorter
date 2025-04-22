using LoLTeamSorter.Application.Contracts.CQRS;

namespace LoLTeamSorter.Application.Commands.DeleteGroup
{
    public record DeleteGroupCommand(Guid Id) : ICommand
    {
    }
}
