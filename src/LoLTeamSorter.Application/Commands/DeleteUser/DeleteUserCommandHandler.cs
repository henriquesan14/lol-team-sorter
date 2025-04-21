using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Exceptions;
using LoLTeamSorter.Domain.ValueObjects;
using MediatR;

namespace LoLTeamSorter.Application.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteUserCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByIdAsync(UserId.Of(request.Id));
            if (user == null) throw new UserNotFoundException(request.Id);

            unitOfWork.Users.Remove(user);
            await unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
