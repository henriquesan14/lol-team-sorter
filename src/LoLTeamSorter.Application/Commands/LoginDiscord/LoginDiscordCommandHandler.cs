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

namespace LoLTeamSorter.Application.Commands.LoginDiscord
{
    public class LoginDiscordCommandHandler(IDiscordAuthService discordAuthService, IUnitOfWork unitOfWork, ITokenService tokenService, IConfiguration configuration) : ICommandHandler<LoginDiscordCommand, AuthResponseViewModel>
    {
        public async Task<AuthResponseViewModel> Handle(LoginDiscordCommand request, CancellationToken cancellationToken)
        {
            var token = await discordAuthService.ObterTokenAsync(request.code);
            if (token is null) throw new IntegrationException("Erro ao obter token do Discord.");

            var userDiscord = await discordAuthService.ObterUsuarioAsync(token.AccessToken);
            if (userDiscord is null) throw new IntegrationException("Erro ao obter usuário do Discord.");

            List<Expression<Func<User, object>>> includes = new List<Expression<Func<User, object>>>
            {
                u => u.Group,
                u => u.Group.Permissions,
            };

            var user = await unitOfWork.Users.GetSingleAsync(u => u.DiscordId == userDiscord.Id, includes: includes);

            if (user is null)
            {
                List<Expression<Func<Group, object>>> includesGroup = new List<Expression<Func<Group, object>>>
                {
                    u => u.Permissions,
                };
                var group = await unitOfWork.Groups.GetSingleAsync(g => g.Name == "MODERADOR", includes: includesGroup);
                user = User.CreateExternal(
                    UserId.Of(Guid.NewGuid()),
                    name: userDiscord.Username,
                    username: Username.Of(userDiscord.Username), 
                    groupId: group.Id,
                    discordId: userDiscord.Id,
                    avatarUrl: $"https://cdn.discordapp.com/avatars/{userDiscord.Id}/{userDiscord.Avatar}.png"
                );

                await unitOfWork.Users.AddAsync(user);
                await unitOfWork.CompleteAsync();
            }

            var jwt = tokenService.GenerateAccessToken(user);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var authResponse = new AuthResponseViewModel
            (
                AccessToken: jwt,
                User: user.ToViewModel()
            );

            var json = JsonSerializer.Serialize(authResponse, options);
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            authResponse.RedirectAppUrl = $"{configuration["Discord:RedirectAppUrl"]}#data={base64}";

            return authResponse;
        }
    }
}
