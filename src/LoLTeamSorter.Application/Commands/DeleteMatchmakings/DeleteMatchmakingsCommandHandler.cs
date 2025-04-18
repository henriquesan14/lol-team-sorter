using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Domain.ValueObjects;
using MediatR;

namespace LoLTeamSorter.Application.Commands.DeleteMatchmakings
{
    public class DeleteMatchmakingsCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteMatchmakingsCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteMatchmakingsCommand request, CancellationToken cancellationToken)
        {
            var matchmakingIds = request.MatchmakingIds.Select(i => MatchmakingId.Of(i)).ToList();
            await unitOfWork.Matchmakings.DeleteRange(matchmakingIds);

            await unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
