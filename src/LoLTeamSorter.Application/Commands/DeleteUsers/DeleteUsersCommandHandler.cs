using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Domain.ValueObjects;
using MediatR;

namespace LoLTeamSorter.Application.Commands.DeleteUsers
{
    public class DeleteUsersCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteUsersCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteUsersCommand request, CancellationToken cancellationToken)
        {
            var userIds = request.UserIds.Select(i => UserId.Of(i)).ToList();
            await unitOfWork.Users.DeleteRange(userIds);

            await unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
