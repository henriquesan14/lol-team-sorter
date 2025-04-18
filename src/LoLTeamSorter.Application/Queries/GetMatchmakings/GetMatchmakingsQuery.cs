using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Pagination;
using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Enums;

namespace LoLTeamSorter.Application.Queries.GetMatchmakings
{
    public record GetMatchmakingsQuery(ModeEnum? Mode, DateTime? StartDate, DateTime? EndDate, int PageNumber, int PageSize) : IQuery<PaginatedResult<MatchmakingViewModel>>
    {
    }
}
