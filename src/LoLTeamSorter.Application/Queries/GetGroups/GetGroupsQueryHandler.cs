using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Extensions;
using LoLTeamSorter.Application.ViewModels;

namespace LoLTeamSorter.Application.Queries.GetGroups
{
    public class GetGroupsQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetGroupsQuery, List<GroupViewModel>>
    {
        public async Task<List<GroupViewModel>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            var groups = await unitOfWork.Groups.GetAllAsync();
            return groups.ToViewModelList().ToList();
        }
    }
}
