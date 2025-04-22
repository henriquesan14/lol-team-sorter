using LoLTeamSorter.Application.Contracts.CQRS;

namespace LoLTeamSorter.Application.Commands.UpdateGroup
{
    public record UpdateGroupCommand(Guid Id, string Name, List<Guid> Permissions) : ICommand;
}
