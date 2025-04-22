using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Exceptions;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using MediatR;
using System.Linq.Expressions;

namespace LoLTeamSorter.Application.Commands.UpdateGroup
{
    public class UpdateGroupCommandCommand(IUnitOfWork unitOfWork) : ICommandHandler<UpdateGroupCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            List<Expression<Func<Group, object>>> includes = new List<Expression<Func<Group, object>>>
            {
                e => e.Permissions
            };
            var group = await unitOfWork.Groups.GetByIdAsync(GroupId.Of(request.Id), includes: includes);
            if (group == null) throw new GroupNotFoundException(request.Id);

            var permissions = new List<Permission>();

            foreach (var id in request.Permissions)
            {
                var permission = await unitOfWork.Permissions.GetByIdAsync(PermissionId.Of(id));
                permissions.Add(permission);
            }

            group.Update(request.Name);
            group.SetPermissions(permissions);

            await unitOfWork.CompleteAsync();
            return Unit.Value;
        }
    }
}
