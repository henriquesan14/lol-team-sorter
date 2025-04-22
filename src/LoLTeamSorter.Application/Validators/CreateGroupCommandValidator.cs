using FluentValidation;
using LoLTeamSorter.Application.Commands.CreateGroup;

namespace LoLTeamSorter.Application.Validators
{
    public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupCommandValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(30).WithMessage("O campo {PropertyName} não pode ter mais de 30 caracteres");

            RuleForEach(x => x.Permissions)
                .NotEmpty().WithMessage("ID inválido (Guid.Empty) encontrado na lista.");
        }
    }
}
