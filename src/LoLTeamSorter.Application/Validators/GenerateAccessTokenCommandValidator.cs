using FluentValidation;
using LoLTeamSorter.Application.Commands.GenerateAccessToken;

namespace LoLTeamSorter.Application.Validators
{
    public class GenerateAccessTokenCommandValidator : AbstractValidator<GenerateAccessTokenCommand>
    {
        public GenerateAccessTokenCommandValidator()
        {
            RuleFor(d => d.Username)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

            RuleFor(d => d.Password)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
        }
    }
}
