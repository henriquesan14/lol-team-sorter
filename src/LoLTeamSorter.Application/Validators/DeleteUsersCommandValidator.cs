using FluentValidation;
using LoLTeamSorter.Application.Commands.DeleteUsers;

namespace LoLTeamSorter.Application.Validators
{
    public class DeleteUsersCommandValidator : AbstractValidator<DeleteUsersCommand>
    {
        public DeleteUsersCommandValidator()
        {
            RuleFor(x => x.UserIds)
            .NotEmpty().WithMessage("A lista de IDs não pode estar vazia.");

            RuleForEach(x => x.UserIds)
                .NotEmpty().WithMessage("ID inválido (Guid.Empty) encontrado na lista.");
        }
    }
}
