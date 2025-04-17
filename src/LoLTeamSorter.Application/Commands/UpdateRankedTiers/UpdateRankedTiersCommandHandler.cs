using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Domain.ValueObjects;
using LoLTeamSorter.Infra.ExternalServices;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LoLTeamSorter.Application.Commands.UpdateRankedTiers
{
    public class UpdateRankedTiersBatchCommandHandler(
    IServiceScopeFactory scopeFactory) : ICommandHandler<UpdateRankedTiersCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateRankedTiersCommand request, CancellationToken cancellationToken)
        {
            var tasks = request.PlayerIds.Select(async playerId =>
            {
                using var scope = scopeFactory.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var riotApiService = scope.ServiceProvider.GetRequiredService<IRiotApiService>();

                var player = await unitOfWork.Players.GetByIdAsync(PlayerId.Of(playerId));
                if (player == null) return;

                var leagues = await riotApiService.GetLeagueByRiotIdAsync(player.RiotId);
                var rankedSolo = leagues.FirstOrDefault(l => l.QueueType == "RANKED_SOLO_5x5");
                var rankedTier = rankedSolo is not null
                    ? RankedTier.Of(rankedSolo.Tier, rankedSolo.Rank)
                    : RankedTier.Unranked();

                player.SetRankedTier(rankedTier);
                await unitOfWork.CompleteAsync();
            });

            await Task.WhenAll(tasks);

            return Unit.Value;
        }
    }
}
