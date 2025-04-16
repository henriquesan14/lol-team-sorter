using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Exceptions;
using LoLTeamSorter.Domain.ValueObjects;
using MediatR;

namespace LoLTeamSorter.Application.Commands.DeletePlayer
{
    public class DeletePlayerCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeletePlayerCommand>
    {
        public async Task<Unit> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = await unitOfWork.Players.GetByIdAsync(PlayerId.Of(request.Id));
            if (player == null) throw new PlayerNotFoundException(request.Id);

            unitOfWork.Players.Remove(player);
            await unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
