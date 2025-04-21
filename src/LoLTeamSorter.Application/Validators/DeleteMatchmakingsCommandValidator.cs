using FluentValidation;
using LoLTeamSorter.Application.Commands.DeleteMatchmakings;

namespace LoLTeamSorter.Application.Validators
{
    public class DeleteMatchmakingsCommandValidator : AbstractValidator<DeleteMatchmakingsCommand>
    {
        public DeleteMatchmakingsCommandValidator()
        {
            RuleFor(x => x.MatchmakingIds)
            .NotEmpty().WithMessage("A lista de IDs não pode estar vazia.");

            RuleForEach(x => x.MatchmakingIds)
                .NotEmpty().WithMessage("ID inválido (Guid.Empty) encontrado na lista.");
        }
    }
}
