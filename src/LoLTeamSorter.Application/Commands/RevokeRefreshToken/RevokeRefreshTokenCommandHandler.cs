using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Contracts.Services;
using LoLTeamSorter.Application.Exceptions;
using MediatR;

namespace LoLTeamSorter.Application.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : ICommandHandler<RevokeRefreshTokenCommand, Unit>
    {
        public async Task<Unit> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = currentUserService.RefreshToken;
            if (string.IsNullOrEmpty(refreshToken))
                throw new RefreshTokenNotFoundException("refreshToken cookie not found");

            var token = await unitOfWork.RefreshTokens.GetSingleAsync(r => r.Token == refreshToken);
            if (token == null)
                throw new RefreshTokenNotFoundException(refreshToken);

            token.Revoke(currentUserService.IpAddress!);
            await unitOfWork.CompleteAsync();

            currentUserService.RemoveCookiesToken();

            return Unit.Value;
        }
    }
}
