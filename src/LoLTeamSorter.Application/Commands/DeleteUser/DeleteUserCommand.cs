using LoLTeamSorter.Application.Contracts.CQRS;

namespace LoLTeamSorter.Application.Commands.DeleteUser
{
    public record DeleteUserCommand(Guid Id) : ICommand
    {
    }
}
