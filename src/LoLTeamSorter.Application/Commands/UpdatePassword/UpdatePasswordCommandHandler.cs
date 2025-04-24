using LoLTeamSorter.Application.Contracts.CQRS;
using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Application.Contracts.Services;
using LoLTeamSorter.Application.Exceptions;
using LoLTeamSorter.Domain.ValueObjects;
using MediatR;

namespace LoLTeamSorter.Application.Commands.UpdatePassword
{
    public class UpdatePasswordCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : ICommandHandler<UpdatePasswordCommand, Unit>
    {
        public async Task<Unit> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByIdAsync(UserId.Of(currentUserService.UserId));

            if (!string.IsNullOrEmpty(user.Password))
            {
                if (string.IsNullOrEmpty(request.CurrentPassword))
                    throw new RequiredCurrentPasswordException("A senha atual é obrigatória.");

                if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.Password))
                    throw new InvalidCurrentPasswordException("Senha atual incorreta.");
            }

            user.UpdatePassword(BCrypt.Net.BCrypt.HashPassword(request.NewPassword, 8));
            await unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
