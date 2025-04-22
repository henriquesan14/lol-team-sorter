using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Exceptions;
using LoLTeamSorter.Application.Extensions;
using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using System.Linq.Expressions;

namespace LoLTeamSorter.Application.Queries.GetGroupById
{
    public class GetGroupByIdQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetGroupByIdQuery, GroupViewModel>
    {
        public async Task<GroupViewModel> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            List<Expression<Func<Group, object>>> includes = new List<Expression<Func<Group, object>>>
            {
                e => e.Permissions
            };
            var group = await unitOfWork.Groups.GetByIdAsync(GroupId.Of(request.id), includes: includes);
            if (group == null) throw new GroupNotFoundException(request.id);

            return group.ToViewModel();
        }
    }
}
