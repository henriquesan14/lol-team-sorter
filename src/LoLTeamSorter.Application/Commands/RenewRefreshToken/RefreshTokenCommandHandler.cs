using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Contracts.Services;
using LoLTeamSorter.Application.Exceptions;
using LoLTeamSorter.Application.Extensions;
using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using System.Linq.Expressions;

namespace LoLTeamSorter.Application.Commands.RenewRefreshToken
{
    public class RefreshTokenCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, ICurrentUserService currentUserService) : ICommandHandler<RefreshTokenCommand, AuthResponseViewModel>
    {
        public async Task<AuthResponseViewModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var existingToken = await unitOfWork.RefreshTokens
                .GetSingleAsync(rt => rt.Token == request.refreshToken);

            if (existingToken is null || existingToken.IsExpired || existingToken.IsRevoked)
            {
                throw new InvalidRefreshTokenException("Sua sessão expirou");
            }

            var includes = new List<Expression<Func<User, object>>>
            {
                u => u.Group,
                u => u.Group.Permissions
            };

            var user = await unitOfWork.Users.GetSingleAsync(u => u.Id == existingToken.UserId, includes: includes);
            if (user is null)
            {
                throw new UserNotFoundException(existingToken.UserId.Value);
            }

            var authToken = tokenService.GenerateAccessToken(user);
            var newRefreshToken = RefreshToken.Create(RefreshTokenId.Of(Guid.NewGuid()), authToken.RefreshToken, user.Id, currentUserService.IpAddress!, DateTime.Now.AddDays(7));

            existingToken.Revoke(currentUserService.IpAddress!);
            existingToken.SetReplacedByToken(newRefreshToken.Token);
            

            await unitOfWork.RefreshTokens.AddAsync(newRefreshToken);
            await unitOfWork.CompleteAsync();

            var response = new AuthResponseViewModel(
                AccessToken: authToken.AccessToken,
                RefreshToken: newRefreshToken.Token,
                RefreshTokenExpiresAt: newRefreshToken.ExpiresAt,
                User: user.ToViewModel(),
                RedirectAppUrl: null
            );

            return response;
        }
    }
}
