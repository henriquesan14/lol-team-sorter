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
            var refreshToken = await unitOfWork.RefreshTokens.GetSingleAsync(r => r.Token == request.refreshToken);
            if (refreshToken == null) throw new RefreshTokenNotFoundException(request.refreshToken);
            refreshToken.Revoke(currentUserService.IpAddress!);

            await unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
