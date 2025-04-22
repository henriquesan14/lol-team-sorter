using LoLTeamSorter.Application.Contracts.CQRS;

namespace LoLTeamSorter.Application.Commands.CreateUser
{
    public record CreateUserCommand(string Name, string Username, string Password, Guid GroupId) : ICommand<Guid>
    {

    }
}
