using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.ViewModels;

namespace LoLTeamSorter.Application.Queries.GetGroupById
{
    public record GetGroupByIdQuery(Guid id) : IQuery<GroupViewModel>;
}
