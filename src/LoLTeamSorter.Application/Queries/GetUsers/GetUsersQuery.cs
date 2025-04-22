using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.ViewModels;

namespace LoLTeamSorter.Application.Queries.GetUsers
{
    public record GetUsersQuery : IQuery<List<UserViewModel>>
    {
    }
}
