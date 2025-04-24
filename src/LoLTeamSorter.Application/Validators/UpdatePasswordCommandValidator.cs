using FluentValidation;
using LoLTeamSorter.Application.Commands.UpdatePassword;

namespace LoLTeamSorter.Application.Validators
{
    public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
    {
        public UpdatePasswordCommandValidator()
        {
            RuleFor(d => d.Password)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MinimumLength(6).WithMessage("O campo {PropertyName} não pode ter menos de 6 caracteres")
                .MaximumLength(30).WithMessage("O campo {PropertyName} não pode ter mais de 30 caracteres");
        }
    }
}
