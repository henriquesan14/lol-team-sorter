using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Extensions;
using LoLTeamSorter.Application.ViewModels;

namespace LoLTeamSorter.Application.Queries.GetUsers
{
    public class GetUsersQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetUsersQuery, List<UserViewModel>>
    {
        public async Task<List<UserViewModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await unitOfWork.Users.GetAsync(includeString: "Group");

            return users.ToViewModelList().ToList();
        }
    }
}
