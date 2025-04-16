﻿using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Exceptions;
using LoLTeamSorter.Domain.ValueObjects;
using LoLTeamSorter.Infra.ExternalServices;
using MediatR;

namespace LoLTeamSorter.Application.Commands.UpdatePlayer
{
    public class UpdatePlayerCommandHandler(IUnitOfWork unitOfWork, IRiotApiService riotApiService) : IRequestHandler<UpdatePlayerCommand, Unit>
    {
        public async Task<Unit> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = await unitOfWork.Players.GetByIdAsync(PlayerId.Of(request.Id));
            if (player == null) throw new PlayerNotFoundException(request.Id);

            var riotIdentifier = RiotIdentifier.Of(request.RiotName, request.RiotTag);
            if (player.RiotIdentifier != riotIdentifier)
            {
                var leagues = await riotApiService.GetLeagueByRiotIdAsync(player.RiotId);
                var rankedSolo = leagues.First(l => l.QueueType.Equals("RANKED_SOLO_5x5"));

                var rankedTier = RankedTier.Of(rankedSolo.Tier, rankedSolo.Rank);
                player.SetRankedTier(rankedTier);
                player.SetRiotIdentifier(riotIdentifier);
            }

            player.Update(request.Name, request.MainLane, request.SecondaryLane, request.Stars);

            await unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
