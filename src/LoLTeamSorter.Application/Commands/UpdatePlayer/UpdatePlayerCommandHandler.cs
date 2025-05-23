﻿using LoLTeamSorter.Application.Contracts.CQRS;
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

            var playerExists = await unitOfWork.Players.GetSingleAsync(
                p => p.RiotIdentifier.Name == request.RiotName &&
                     p.RiotIdentifier.Tag == request.RiotTag
            );
            if (playerExists != null && player.Id != playerExists.Id) throw new PlayerAlreadyExistsException(playerExists.RiotIdentifier.ToString());

            var riotIdentifier = RiotIdentifier.Of(request.RiotName, request.RiotTag);
            if (player.RiotIdentifier != riotIdentifier)
            {
                var account = await riotApiService.GetAccountByRiotIdAsync(request.RiotName, request.RiotTag);
                var leagues = await riotApiService.GetLeagueByRiotIdAsync(account.Puuid);
                var rankedSolo = leagues.FirstOrDefault(l => l.QueueType.Equals("RANKED_SOLO_5x5"));
                var rankedTier = rankedSolo is not null
                    ? RankedTier.Of(rankedSolo.Tier, rankedSolo.Rank)
                    : RankedTier.Unranked();

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
