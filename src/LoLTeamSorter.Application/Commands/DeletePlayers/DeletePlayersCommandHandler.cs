using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Domain.ValueObjects;
using MediatR;

namespace LoLTeamSorter.Application.Commands.DeletePlayers
{
    public class DeletePlayersCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeletePlayersCommand, Unit>
    {
        public async Task<Unit> Handle(DeletePlayersCommand request, CancellationToken cancellationToken)
        {
            var playerIds = request.PlayerIds.Select(i => PlayerId.Of(i)).ToList();
            await unitOfWork.Players.DeleteRange(playerIds);

            await unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
