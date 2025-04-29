using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.ViewModels;

namespace LoLTeamSorter.Application.Commands.RenewRefreshToken
{
    public record RefreshTokenCommand : ICommand<AuthResponseViewModel>;
}
