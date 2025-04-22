using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Domain.ValueObjects;
using MediatR;

namespace LoLTeamSorter.Application.Commands.DeleteGroups
{
    public class DeleteGroupsCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteGroupsCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteGroupsCommand request, CancellationToken cancellationToken)
        {
            var groupIds = request.groupIds.Select(i => GroupId.Of(i)).ToList();
            await unitOfWork.Groups.DeleteRange(groupIds);

            await unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
