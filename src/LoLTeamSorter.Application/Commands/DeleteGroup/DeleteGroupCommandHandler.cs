using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Exceptions;
using LoLTeamSorter.Domain.ValueObjects;
using MediatR;

namespace LoLTeamSorter.Application.Commands.DeleteGroup
{
    public class DeleteGroupCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteGroupCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await unitOfWork.Groups.GetByIdAsync(GroupId.Of(request.Id));
            if (group == null) throw new GroupNotFoundException(request.Id);

            unitOfWork.Groups.Remove(group);
            await unitOfWork.CompleteAsync();
            return Unit.Value;
        }
    }
}
