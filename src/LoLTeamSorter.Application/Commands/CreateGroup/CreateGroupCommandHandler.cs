using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Exceptions;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Application.Commands.CreateGroup
{
    public class CreateGroupCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateGroupCommand, Guid>
    {
        public async Task<Guid> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var groupExists = await unitOfWork.Groups.GetSingleAsync(g => g.Name.ToLower() == request.Name.ToLower());
            if (groupExists != null) throw new GroupAlreadyExistsException(request.Name);

            var group = Group.Create(GroupId.Of(Guid.NewGuid()),request.Name);
            foreach (var id in request.Permissions)
            {
                var permissao = await unitOfWork.Permissions.GetByIdAsync(PermissionId.Of(id));
                group.AddPermission(permissao);
            }

            await unitOfWork.Groups.AddAsync(group);
            await unitOfWork.CompleteAsync();

            return group.Id.Value;
        }
    }
}
