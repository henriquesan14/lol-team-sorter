using LoLTeamSorter.Application.Contracts.CQRS;

namespace LoLTeamSorter.Application.Commands.DeleteUsers
{
    public record DeleteUsersCommand(List<Guid> UserIds) : ICommand
    {
    }
}
