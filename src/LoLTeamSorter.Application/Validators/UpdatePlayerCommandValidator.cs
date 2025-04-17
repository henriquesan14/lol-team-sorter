using FluentValidation;
using LoLTeamSorter.Application.Commands.UpdatePlayer;

namespace LoLTeamSorter.Application.Validators
{
    public class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
    {
        public UpdatePlayerCommandValidator()
        {
            RuleFor(d => d.Id)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
 
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(30).WithMessage("O campo {PropertyName} não pode ter mais de 30 caracteres");

            RuleFor(d => d.RiotName)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(30).WithMessage("O campo {PropertyName} não pode ter mais de 30 caracteres");

            RuleFor(d => d.RiotTag)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .Length(3, 5)
                .WithMessage("O campo {PropertyName} deve ter entre 3 e 5 caracteres.");

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
