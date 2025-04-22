using LoLTeamSorter.Application.Contracts.CQRS;

namespace LoLTeamSorter.Application.Commands.CreateGroup
{
    public record CreateGroupCommand(string Name, List<Guid> Permissions) : ICommand<Guid>;
}
