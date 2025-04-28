using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Exceptions;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using MediatR;
using System.Linq.Expressions;

namespace LoLTeamSorter.Application.Commands.FinishMatch
{
    public class FinishMatchCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<FinishMatchCommand, Unit>
    {
        public async Task<Unit> Handle(FinishMatchCommand request, CancellationToken cancellationToken)
        {
            List<Expression<Func<Matchmaking, object>>> includes = new List<Expression<Func<Matchmaking, object>>>() { 
                m => m.BlueTeam,
                m => m.BlueTeam.Players,
                m => m.RedTeam,
                m => m.RedTeam.Players,
            };
            var matchmaking = await unitOfWork.Matchmakings.GetByIdAsync(MatchmakingId.Of(request.MatchmakingId), includes: includes);
            if (matchmaking is null) throw new MatchmakingNotFoundException(request.MatchmakingId);
            
            Team? winner = null;
            var winningTeamId = TeamId.Of(request.WinningTeamId);

            if (matchmaking.BlueTeam.Id == winningTeamId)
            {
                winner = matchmaking.BlueTeam;
            }
            else if (matchmaking.RedTeam.Id == winningTeamId)
            {
                winner = matchmaking.RedTeam;
            }

            if (winner is null) throw new InvalidWinningTeamException("Time vencedor inválido");

            await unitOfWork.BeginTransaction();
            matchmaking.FinishMatch(winner);

            await unitOfWork.CompleteAsync();
            await unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
