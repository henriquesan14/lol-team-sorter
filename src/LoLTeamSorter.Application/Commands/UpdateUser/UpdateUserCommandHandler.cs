using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Exceptions;
using LoLTeamSorter.Domain.ValueObjects;
using MediatR;

namespace LoLTeamSorter.Application.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<UpdateUserCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByIdAsync(UserId.Of(request.Id));
            if (user == null) throw new UserNotFoundException(request.Id);

            var userExists = await unitOfWork.Users.GetSingleAsync(
                p => p.Username == Username.Of(request.Username)
            );

            if (userExists != null && user.Id != userExists.Id) throw new UserAlreadyExistsException(userExists.Username.Value);

            user.Update(request.Name, Username.Of(request.Username), GroupId.Of(request.GroupId));

            if(request.Password != null) user.UpdatePassword(BCrypt.Net.BCrypt.HashPassword(request.Password, 8));

            await unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
