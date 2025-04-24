using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.ViewModels;

namespace LoLTeamSorter.Application.Commands.LoginDiscord
{
    public record LoginDiscordCommand(string code) : ICommand<AuthResponseViewModel>
    {
    }
}
