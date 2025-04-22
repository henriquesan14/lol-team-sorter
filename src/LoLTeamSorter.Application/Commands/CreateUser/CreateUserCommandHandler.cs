using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Exceptions;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using System.Linq.Expressions;

namespace LoLTeamSorter.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateUserCommand, Guid>
    {
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<User, bool>> predicate = u => u.Username == Username.Of(request.Username);
            var username = await unitOfWork.Users.GetSingleAsync(predicate);
            if (username != null) throw new UserAlreadyExistsException(request.Username);

            var user = User.Create(
                id: UserId.Of(Guid.NewGuid()),
                name: request.Name,
                username: Username.Of(request.Username),
                password: BCrypt.Net.BCrypt.HashPassword(request.Password, 8),
                groupId: GroupId.Of(request.GroupId)
            );

            await unitOfWork.Users.AddAsync(user);
            await unitOfWork.CompleteAsync();

            return user.Id.Value;
        }
    }
}
