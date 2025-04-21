using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.ViewModels;

namespace LoLTeamSorter.Application.Queries.GetGroups
{
    public class GetGroupsQuery : IQuery<List<GroupViewModel>>
    {
    }
}
