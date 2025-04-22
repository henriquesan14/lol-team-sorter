using LoLTeamSorter.Application.Contracts.CQRS;

namespace LoLTeamSorter.Application.Commands.UpdateUser
{
    public record UpdateUserCommand(Guid Id, string Name, string Username, string Password, Guid GroupId) : ICommand
    {
    }
}
