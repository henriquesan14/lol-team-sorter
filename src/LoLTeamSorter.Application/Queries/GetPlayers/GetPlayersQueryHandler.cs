using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Extensions;
using LoLTeamSorter.Application.ViewModels;
using MediatR;

namespace LoLTeamSorter.Application.Queries.GetPlayers
{
    public class GetPlayersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPlayersQuery, IEnumerable<PlayerViewModel>>
    {
        public async Task<IEnumerable<PlayerViewModel>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
        {
            var players = await unitOfWork.Players.GetAllAsync();

            return players.ToViewModelList();
        }
    }
}
