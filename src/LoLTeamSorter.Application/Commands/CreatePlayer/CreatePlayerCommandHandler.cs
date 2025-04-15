using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using MediatR;

namespace LoLTeamSorter.Application.Commands.CreatePlayer
{
    public class CreatePlayerCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatePlayerCommand, Guid>
    {
        public async Task<Guid> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var riotId = "";
            var player = Player.Create(
                PlayerId.Of(Guid.NewGuid()),
                request.Name,
                RiotIdentifier.Of(request.RiotName, request.RiotTag),
                request.MainLane,
                request.SecondaryLane,
                RankedTier.Of(request.Tier, request.Rank),
                request.Stars,
                riotId
            );

            await unitOfWork.Players.AddAsync( player );
            await unitOfWork.CompleteAsync();

            return player.Id.Value;
        }
    }
}
