using FluentValidation;
using LoLTeamSorter.Application.Commands.CreateUser;

namespace LoLTeamSorter.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(30).WithMessage("O campo {PropertyName} não pode ter mais de 30 caracteres");

            RuleFor(d => d.Username)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MinimumLength(6).WithMessage("O campo {PropertyName} não pode ter menos de 6 caracteres")
                .MaximumLength(30).WithMessage("O campo {PropertyName} não pode ter mais de 30 caracteres")
                .Matches("^[a-zA-Z0-9_-]+$").WithMessage("O {PropertyName} só pode conter letras, números, hífen e underline.");

            RuleFor(d => d.Password)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MinimumLength(6).WithMessage("O campo {PropertyName} não pode ter menos de 6 caracteres")
                .MaximumLength(30).WithMessage("O campo {PropertyName} não pode ter mais de 30 caracteres");

            RuleFor(d => d.GroupId)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
        }
    }
}
