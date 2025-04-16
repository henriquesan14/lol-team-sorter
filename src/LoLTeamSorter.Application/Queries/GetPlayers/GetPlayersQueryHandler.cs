using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Extensions;
using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Entities;
using MediatR;

namespace LoLTeamSorter.Application.Queries.GetPlayers
{
    public class GetPlayersQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetPlayersQuery, IEnumerable<PlayerViewModel>>
    {
        public async Task<IEnumerable<PlayerViewModel>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
        {
            Func<IQueryable<Player>, IOrderedQueryable<Player>> orderBy = p => p.OrderByDescending(x => x.Stars);

            var players = await unitOfWork.Players.GetAllAsync(orderBy);

            return players.ToViewModelList();
        }
    }
}
