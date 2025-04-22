using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.ViewModels;

namespace LoLTeamSorter.Application.Commands.GenerateAccessToken
{
    public record GenerateAccessTokenCommand(string? Username, string? Password) : ICommand<AuthResponseViewModel>;
}
