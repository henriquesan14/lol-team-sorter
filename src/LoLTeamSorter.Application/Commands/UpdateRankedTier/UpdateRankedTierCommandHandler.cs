using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Exceptions;
using LoLTeamSorter.Domain.ValueObjects;
using LoLTeamSorter.Infra.ExternalServices;
using MediatR;

namespace LoLTeamSorter.Application.Commands.UpdateRankedTier
{
    public class UpdateRankedTierCommandHandler(IUnitOfWork unitOfWork, IRiotApiService riotApiService) : ICommandHandler<UpdateRankedTierCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateRankedTierCommand request, CancellationToken cancellationToken)
        {
            var player = await unitOfWork.Players.GetByIdAsync(PlayerId.Of(request.Id));
            if (player == null) throw new PlayerNotFoundException(request.Id);

            
            var leagues = await riotApiService.GetLeagueByRiotIdAsync(player.RiotId);
            var rankedSolo = leagues.FirstOrDefault(l => l.QueueType.Equals("RANKED_SOLO_5x5"));
            var rankedTier = rankedSolo is not null
                ? RankedTier.Of(rankedSolo.Tier, rankedSolo.Rank)
                : RankedTier.Unranked();

            player.SetRankedTier(rankedTier);

            await unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
