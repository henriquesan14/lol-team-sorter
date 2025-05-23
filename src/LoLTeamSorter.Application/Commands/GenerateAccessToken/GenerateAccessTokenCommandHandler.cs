﻿using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Contracts.Services;
using LoLTeamSorter.Application.Exceptions;
using LoLTeamSorter.Application.Extensions;
using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using System.Linq.Expressions;

namespace LoLTeamSorter.Application.Commands.GenerateAccessToken
{
    public class GenerateAccessTokenCommandHandler(ITokenService tokenService, IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : ICommandHandler<GenerateAccessTokenCommand, UserViewModel>
    {
        public async Task<UserViewModel> Handle(GenerateAccessTokenCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<User, bool>> predicate = u => u.Username == Username.Of(request.Username!);
            List<Expression<Func<User, object>>> includes = new List<Expression<Func<User, object>>>
            {
                u => u.Group,
                u => u.Group.Permissions,
            };
            var userExists = await unitOfWork.Users.GetSingleAsync(predicate, includes: includes);
            if (userExists == null)
                throw new UnauthorizedException();
            bool password = BCrypt.Net.BCrypt.Verify(request.Password, userExists.Password);
            if (!password)
            {
                throw new UnauthorizedException();
            }
            var authToken = tokenService.GenerateAccessToken(userExists);

            var refreshToken = RefreshToken.Create(
                id: RefreshTokenId.Of(Guid.NewGuid()),
                token: authToken.RefreshToken,
                userId: UserId.Of(userExists.Id.Value),
                expiresAt: authToken.RefreshTokenExpiresAt,
                createdByIp: currentUserService.IpAddress!
                );
            await unitOfWork.RefreshTokens.AddAsync(refreshToken);
            await unitOfWork.CompleteAsync();

            currentUserService.SetCookieTokens(authToken.AccessToken, authToken.RefreshToken);

            return userExists.ToViewModel();
        }
    }
}
