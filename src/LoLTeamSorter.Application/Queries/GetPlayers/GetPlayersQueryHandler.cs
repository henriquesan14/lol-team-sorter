using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Extensions;
using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace LoLTeamSorter.Application.Queries.GetPlayers
{
    public class GetPlayersQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetPlayersQuery, IEnumerable<PlayerViewModel>>
    {
        public async Task<IEnumerable<PlayerViewModel>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
        {
            var direction = request.Descending ? "descending" : "ascending";

            Func<IQueryable<Player>, IOrderedQueryable<Player>> orderBy = players =>
                players.OrderBy($"{request.OrderBy} {direction}");

            var players = await unitOfWork.Players.GetAllAsync(orderBy);

            return players.ToViewModelList();
        }
    }
}
