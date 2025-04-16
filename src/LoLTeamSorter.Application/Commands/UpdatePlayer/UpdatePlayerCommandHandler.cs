using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Exceptions;
using LoLTeamSorter.Domain.ValueObjects;
using LoLTeamSorter.Infra.ExternalServices;
using MediatR;

namespace LoLTeamSorter.Application.Commands.UpdatePlayer
{
    public class UpdatePlayerCommandHandler(IUnitOfWork unitOfWork, IRiotApiService riotApiService) : ICommandHandler<UpdatePlayerCommand, Unit>
    {
        public async Task<Unit> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = await unitOfWork.Players.GetByIdAsync(PlayerId.Of(request.Id));
            if (player == null) throw new PlayerNotFoundException(request.Id);

            var riotIdentifier = RiotIdentifier.Of(request.RiotName, request.RiotTag);
            if (player.RiotIdentifier != riotIdentifier)
            {
                var account = await riotApiService.GetAccountByRiotIdAsync(request.RiotName, request.RiotTag);
                var leagues = await riotApiService.GetLeagueByRiotIdAsync(account.Puuid);
                var rankedSolo = leagues.First(l => l.QueueType.Equals("RANKED_SOLO_5x5"));

                var rankedTier = RankedTier.Of(rankedSolo.Tier, rankedSolo.Rank);
                player.SetRankedTier(rankedTier);
                player.SetRiotIdentifier(riotIdentifier);
                player.SetRiotId(account.Puuid);
            }

            player.Update(request.Name, request.MainLane, request.SecondaryLane, request.Stars);

            await unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
