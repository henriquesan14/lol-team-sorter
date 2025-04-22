using LoLTeamSorter.Application.Contracts.CQRS;

namespace LoLTeamSorter.Application.Commands.DeleteGroups
{
    public record DeleteGroupsCommand(List<Guid> groupIds) : ICommand
    {
    }
}
