using LoLTeamSorter.Application.Contracts.CQRS;

namespace LoLTeamSorter.Application.Commands.UpdatePassword
{
    public record UpdatePasswordCommand(string? CurrentPassword, string Password) : ICommand
    {
    }
}
