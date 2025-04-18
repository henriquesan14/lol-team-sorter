using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Exceptions;
using LoLTeamSorter.Domain.ValueObjects;
using MediatR;

namespace LoLTeamSorter.Application.Commands.DeleteMatchmaking
{
    public class DeleteMatchmakingCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteMatchmakingCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteMatchmakingCommand request, CancellationToken cancellationToken)
        {
            var matchmaking = await unitOfWork.Matchmakings.GetByIdAsync(MatchmakingId.Of(request.Id));
            if (matchmaking == null) throw new MatchmakingNotFoundException(request.Id);

            unitOfWork.Matchmakings.Remove(matchmaking);
            await unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
