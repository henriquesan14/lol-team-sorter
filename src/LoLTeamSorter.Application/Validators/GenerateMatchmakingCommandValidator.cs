using FluentValidation;
using LoLTeamSorter.Application.Commands.GenerateMatchmaking;

namespace LoLTeamSorter.Application.Validators
{
    public class GenerateMatchmakingCommandValidator : AbstractValidator<GenerateMatchmakingCommand>
    {
        public GenerateMatchmakingCommandValidator()
        {
            RuleFor(x => x.Mode)
                .IsInEnum()
                .WithMessage("Modo inválido.");

            RuleFor(x => x.PlayerIds)
                .NotNull().WithMessage("A lista de jogadores é obrigatória.")
                .Must(ids => ids.Count == 10)
                .WithMessage("A lista de jogadores deve conter exatamente 10 jogadores.")
                .Must(ids => ids.Distinct().Count() == ids.Count)
                .WithMessage("A lista de jogadores contém GUIDs duplicados.")
                .Must(ids => ids.All(id => id != Guid.Empty))
                .WithMessage("Todos os IDs de jogadores devem ser válidos (não vazios).");
        }
    }
}
