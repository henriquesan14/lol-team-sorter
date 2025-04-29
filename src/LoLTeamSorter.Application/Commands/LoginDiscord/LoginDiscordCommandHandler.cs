using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Contracts.Services;
using LoLTeamSorter.Application.Exceptions;
using LoLTeamSorter.Application.Extensions;
using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using LoLTeamSorter.Application.Contracts.Services.Response;

namespace LoLTeamSorter.Application.Commands.LoginDiscord
{
    public class LoginDiscordCommandHandler(IDiscordAuthService discordAuthService, IUnitOfWork unitOfWork, ITokenService tokenService,
        IConfiguration configuration, ICurrentUserService currentUserService) : ICommandHandler<LoginDiscordCommand, AuthResponseViewModel>
    {
        public async Task<AuthResponseViewModel> Handle(LoginDiscordCommand request, CancellationToken cancellationToken)
        {
            var token = await discordAuthService.ObterTokenAsync(request.code);
            if (token is null) throw new IntegrationException("Erro ao obter token do Discord.");

            var userDiscord = await discordAuthService.ObterUsuarioAsync(token.AccessToken);
            if (userDiscord is null) throw new IntegrationException("Erro ao obter usuário do Discord.");

            var avatarUrl = $"https://cdn.discordapp.com/avatars/{userDiscord.Id}/{userDiscord.Avatar}.png";

            List<Expression<Func<User, object>>> userIncludes = new List<Expression<Func<User, object>>>
            {
                u => u.Group,
                u => u.Group.Permissions,
            };

            var user = await unitOfWork.Users.GetSingleAsync(u => u.DiscordId == userDiscord.Id, includes: userIncludes);

            if (user is null)
            {
                var existingByUsername = await unitOfWork.Users.GetSingleAsync(u => u.Username == Username.Of(userDiscord.Username), includes: userIncludes);

                if (existingByUsername is not null)
                {
                    existingByUsername.SetDiscordId(userDiscord.Id);
                    if (HasAvatar(userDiscord)) existingByUsername.SetAvatarUrl(avatarUrl);
                    existingByUsername.SetExternalLogin(true);

                    user = existingByUsername;

                    await unitOfWork.CompleteAsync();
                }
                else
                {
                    List<Expression<Func<Group, object>>> groupIncludes = new List<Expression<Func<Group, object>>>
                    {
                        u => u.Permissions,
                    };

                    var group = await unitOfWork.Groups.GetSingleAsync(g => g.Name == "MODERADOR", includes: groupIncludes);

                    user = User.CreateExternal(
                        UserId.Of(Guid.NewGuid()),
                        name: userDiscord.Username,
                        username: Username.Of(userDiscord.Username),
                        groupId: group.Id,
                        discordId: userDiscord.Id
                    );

                    if (HasAvatar(userDiscord)) user.SetAvatarUrl(avatarUrl);

                    await unitOfWork.Users.AddAsync(user);
                }
            }

            var authToken = tokenService.GenerateAccessToken(user);

            var refreshToken = RefreshToken.Create(
                id: RefreshTokenId.Of(Guid.NewGuid()),
                token: authToken.RefreshToken,
                userId: UserId.Of(user.Id.Value),
                expiresAt: authToken.RefreshTokenExpiresAt,
                createdByIp: currentUserService.IpAddress!
                );
            await unitOfWork.RefreshTokens.AddAsync(refreshToken);
            await unitOfWork.CompleteAsync();

            currentUserService.SetCookieTokens(authToken.AccessToken, authToken.RefreshToken);

            return new AuthResponseViewModel
            (
                AccessToken: authToken.AccessToken,
                RefreshToken: authToken.RefreshToken,
                RefreshTokenExpiresAt: authToken.RefreshTokenExpiresAt,
                User: user.ToViewModel(),
                RedirectAppUrl: GenerateRedirectUrl(authToken.AccessToken, authToken.RefreshToken, authToken.RefreshTokenExpiresAt, user)
            );
        }

        private bool HasAvatar(DiscordUserResponse userDiscord) =>
    !       string.IsNullOrEmpty(userDiscord.Avatar);

        private string GenerateRedirectUrl(string accessToken, string refreshToken, DateTime refreshTokenExpiresAt, User user)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                RefreshTokenExpiresAt = refreshTokenExpiresAt,
                User = user.ToViewModel()
            }, options);

            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            return $"{configuration["Discord:RedirectAppUrl"]}#data={base64}";
        }
    }
}
