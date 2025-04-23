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
                    context.SetDrawStrategy(new DrawByStars());
                    break;
                case ModeEnum.TIER:
                    context.SetDrawStrategy(new DrawByTier());
                    break;
                case ModeEnum.LANE:
                    context.SetDrawStrategy(new DrawByLane());
                    break;
                case ModeEnum.RANDOM:
                    context.SetDrawStrategy(new DrawByRandom());
                    break;
                default:
                    throw new ArgumentException("Strategy not found");
            }

            // Balancear os times com a estratégia escolhida
            return context.BalanceTeams(players);
        }
    }
}
