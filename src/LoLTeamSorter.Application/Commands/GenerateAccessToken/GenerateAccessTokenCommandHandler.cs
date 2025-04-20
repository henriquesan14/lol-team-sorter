using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Contracts.Services;
using LoLTeamSorter.Application.Extensions;
using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using System.Linq.Expressions;

namespace LoLTeamSorter.Application.Commands.GenerateAccessToken
{
    public class GenerateAccessTokenCommandHandler(ITokenService tokenService, IUnitOfWork unitOfWork) : ICommandHandler<GenerateAccessTokenCommand, AuthResponseViewModel>
    {
        public async Task<AuthResponseViewModel> Handle(GenerateAccessTokenCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<User, bool>> predicate = u => u.Username == Username.Of(request.Username!);
            List<Expression<Func<User, object>>> includes = new List<Expression<Func<User, object>>>
            {
                u => u.Group,
                u => u.Group.Permissions,
            };
            var userExists = await unitOfWork.Users.GetSingleAsync(predicate, includes: includes);
            if (userExists == null)
                throw new Exception("Username or password are incorrect");
            bool password = BCrypt.Net.BCrypt.Verify(request.Password, userExists.Password);
            if (!password)
            {
                throw new Exception("Username or password are incorrect");
            }
            var accesstoken = tokenService.GenerateAccessToken(userExists);
            var viewModel = new AuthResponseViewModel(accesstoken, userExists.ToViewModel());
            return viewModel;
        }
    }
}
