using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Exceptions;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using LoLTeamSorter.Infra.ExternalServices;

namespace LoLTeamSorter.Application.Commands.CreatePlayer
{
    public class CreatePlayerCommandHandler(IUnitOfWork unitOfWork, IRiotApiService riotApiService) : ICommandHandler<CreatePlayerCommand, Guid>
    {
        public async Task<Guid> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var playerExists = await unitOfWork.Players.GetSingleAsync(
                p => p.RiotIdentifier.Name == request.RiotName &&
                     p.RiotIdentifier.Tag == request.RiotTag
            );
            
            if (playerExists != null) throw new PlayerAlreadyExistsException(playerExists.RiotIdentifier.ToString());
            var account = await riotApiService.GetAccountByRiotIdAsync(request.RiotName, request.RiotTag);
            var leagues = await riotApiService.GetLeagueByRiotIdAsync(account.Puuid);
            var rankedSolo = leagues.First(l => l.QueueType.Equals("RANKED_SOLO_5x5"));

            var rankedTier = RankedTier.Of(rankedSolo.Tier, rankedSolo.Rank);

            var player = Player.Create(
                PlayerId.Of(Guid.NewGuid()),
                request.Name,
                RiotIdentifier.Of(request.RiotName, request.RiotTag),
                request.MainLane,
                request.SecondaryLane,
                rankedTier,
                request.Stars,
                account.Puuid
            );

            await unitOfWork.Players.AddAsync( player );
            await unitOfWork.CompleteAsync();

            return player.Id.Value;
        }
    }
}
