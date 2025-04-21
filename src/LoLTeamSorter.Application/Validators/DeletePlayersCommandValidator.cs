using FluentValidation;
using LoLTeamSorter.Application.Commands.DeletePlayers;

namespace LoLTeamSorter.Application.Validators
{
    public class DeletePlayersCommandValidator : AbstractValidator<DeletePlayersCommand>
    {
        public DeletePlayersCommandValidator()
        {
            RuleFor(x => x.PlayerIds)
            .NotEmpty().WithMessage("A lista de IDs não pode estar vazia.");

            RuleForEach(x => x.PlayerIds)
                .NotEmpty().WithMessage("ID inválido (Guid.Empty) encontrado na lista.");
        }
    }
}
