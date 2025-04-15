using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Extensions;
using LoLTeamSorter.Application.ViewModels;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.Exceptions;
using LoLTeamSorter.Domain.Services;
using LoLTeamSorter.Domain.ValueObjects;
using MediatR;

namespace LoLTeamSorter.Application.Commands.GenerateMatchmaking
{
    public class GenerateMatchmakingCommandHandler : IRequestHandler<GenerateMatchmakingCommand, MatchmakingViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenerateMatchmakingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MatchmakingViewModel> Handle(GenerateMatchmakingCommand request, CancellationToken cancellationToken)
        {
            List<Player> players = new();
            foreach (var id in request.PlayerIds) {
                var player = await _unitOfWork.Players.GetByIdAsync(PlayerId.Of(id));
                if (player == null)
                    throw new DomainException("One or more players were not found.");
                players.Add(player);
            }
                
            List<Player> blueTeamPlayers;
            List<Player> redTeamPlayers;

            if (request.Mode == Domain.Enums.ModeEnum.STARS)
            {
                (blueTeamPlayers, redTeamPlayers) = MatchmakingLogic.BalanceByStars(players);
            }
            else if (request.Mode == Domain.Enums.ModeEnum.TIER)
            {
                (blueTeamPlayers, redTeamPlayers) = MatchmakingLogic.BalanceByTier(players);
            }
            else
            {
                throw new DomainException("Invalid matchmaking mode.");
            }

            var blueTeam = Team.Create(TeamId.Of(Guid.NewGuid()), blueTeamPlayers);
            var redTeam = Team.Create(TeamId.Of(Guid.NewGuid()), redTeamPlayers);

            var matchmaking = Matchmaking.Create(
                MatchmakingId.Of(Guid.NewGuid()),
                request.Mode,
                blueTeam,
                redTeam
            );

            await _unitOfWork.Matchmakings.AddAsync(matchmaking);
            await _unitOfWork.CompleteAsync();

            return matchmaking.ToViewModel();
        }
    }
}
