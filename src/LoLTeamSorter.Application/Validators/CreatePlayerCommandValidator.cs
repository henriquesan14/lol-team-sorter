using FluentValidation;
using LoLTeamSorter.Application.Commands.CreatePlayer;

namespace LoLTeamSorter.Application.Validators
{
    public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
    {
        public CreatePlayerCommandValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(30).WithMessage("O campo {PropertyName} não pode ter mais de 30 caracteres");

            RuleFor(d => d.RiotName)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(30).WithMessage("O campo {PropertyName} não pode ter mais de 30 caracteres");

            RuleFor(d => d.RiotTag)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .Length(3).WithMessage("O campo {PropertyName} tem q ter exatamente 3 caracteres");

            RuleFor(d => d.RiotTag)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .Length(3).WithMessage("O campo {PropertyName} tem q ter exatamente 3 caracteres");

            RuleFor(d => d.MainLane)
                .NotNull().WithMessage("O campo {PropertyName} é obrigatório")
                .IsInEnum().WithMessage("O campo {PropertyName} não é válido");

            RuleFor(d => d.SecondaryLane)
                .NotNull().WithMessage("O campo {PropertyName} é obrigatório")
                .IsInEnum().WithMessage("O campo {PropertyName} não é válido");

            RuleFor(d => d.Stars)
                .InclusiveBetween(1, 5).WithMessage("O campo {PropertyName} deve estar entre 1 e 5.");
        }
    }
}
