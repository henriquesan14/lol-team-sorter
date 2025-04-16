using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Extensions;
using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace LoLTeamSorter.Application.Queries.GetMatchmakings
{
    public class GetMatchmakingsQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetMatchmakingsQuery, IEnumerable<MatchmakingViewModel>>
    {
        public async Task<IEnumerable<MatchmakingViewModel>> Handle(GetMatchmakingsQuery request, CancellationToken cancellationToken)
        {
            List<Expression<Func<Matchmaking, object>>> includes = new List<Expression<Func<Matchmaking, object>>>
            {
                e => e.BlueTeam,
                e => e.BlueTeam.Players,
                e => e.RedTeam,
                e => e.RedTeam.Players,
            };
            var matchmakings = await unitOfWork.Matchmakings.GetAsync(includes: includes);

            return matchmakings.ToViewModelList();
        }
    }
}
