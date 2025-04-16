using LoLTeamSorter.Application.Contexts;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.Enums;
using LoLTeamSorter.Domain.Strategies;

namespace LoLTeamSorter.Application.Services
{
    public class MatchmakingService
    {
        public (List<Player> BlueTeam, List<Player> RedTeam) BalanceTeams(List<Player> players, ModeEnum strategy)
        {
            var context = new MatchmakingContext();

            // Escolher a estratégia com base no parâmetro
            switch (strategy)
            {
                case ModeEnum.STARS:
                    context.SetBalanceStrategy(new BalanceByStars());
                    break;
                case ModeEnum.TIER:
                    context.SetBalanceStrategy(new BalanceByTier());
                    break;
                case ModeEnum.LANE:
                    context.SetBalanceStrategy(new BalanceByLane());
                    break;
                case ModeEnum.RANDOM:
                    context.SetBalanceStrategy(new BalanceRandom());
                    break;
                default:
                    throw new ArgumentException("Strategy not found");
            }

            // Balancear os times com a estratégia escolhida
            return context.BalanceTeams(players);
        }
    }
}
