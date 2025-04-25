using LoLTeamSorter.Application.Contracts.CQRS;

namespace LoLTeamSorter.Application.Commands.RevokeRefreshToken
{
    public record RevokeRefreshTokenCommand(string refreshToken) : ICommand
    {
    }
}
