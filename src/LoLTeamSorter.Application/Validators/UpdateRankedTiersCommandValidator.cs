using FluentValidation;
using LoLTeamSorter.Application.Commands.UpdateRankedTiers;

namespace LoLTeamSorter.Application.Validators
{
    public class UpdateRankedTiersCommandValidator : AbstractValidator<UpdateRankedTiersCommand>
    {
        public UpdateRankedTiersCommandValidator()
        {
            RuleFor(x => x.PlayerIds)
            .NotEmpty().WithMessage("A lista de IDs não pode estar vazia.");

            RuleForEach(x => x.PlayerIds)
                .NotEmpty().WithMessage("ID inválido (Guid.Empty) encontrado na lista.");
        }
    }
}
