using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Extensions;
using LoLTeamSorter.Application.Pagination;
using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Entities;
using System.Linq.Expressions;

namespace LoLTeamSorter.Application.Queries.GetMatchmakings
{
    public class GetMatchmakingsQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetMatchmakingsQuery, PaginatedResult<MatchmakingViewModel>>
    {
        public async Task<PaginatedResult<MatchmakingViewModel>> Handle(GetMatchmakingsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Matchmaking, bool>> predicate = m => (!request.Mode.HasValue || m.Mode == request.Mode) &&
                (!request.StartDate.HasValue || m.CreatedAt!.Value.Date >= request.StartDate.Value.Date) &&
                (!request.EndDate.HasValue || m.CreatedAt!.Value.Date <= request.EndDate.Value.Date);
            List<Expression<Func<Matchmaking, object>>> includes = new List<Expression<Func<Matchmaking, object>>>
            {
                e => e.BlueTeam,
                e => e.BlueTeam.Players,
                e => e.RedTeam,
                e => e.RedTeam.Players,
                e => e.WinningTeam!,
                e => e.WinningTeam!.Players,
            };

            Func<IQueryable<Matchmaking>, IOrderedQueryable<Matchmaking>> orderBy = m => m.OrderByDescending(mm => mm.CreatedAt!.Value);
            var matchmakings = await unitOfWork.Matchmakings.GetAsync(predicate: predicate, orderBy: orderBy, includes: includes, pageNumber: request.PageNumber, pageSize: request.PageSize);
            var countMatchMakings = await unitOfWork.Matchmakings.GetCountAsync(predicate);

            return new PaginatedResult<MatchmakingViewModel>(request.PageNumber, request.PageSize, countMatchMakings, matchmakings.ToViewModelList());
        }
    }
}
