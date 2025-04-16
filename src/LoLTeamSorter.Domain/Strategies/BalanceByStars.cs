using LoLTeamSorter.Domain.Entities;

namespace LoLTeamSorter.Domain.Strategies
{
    public class BalanceByStars : IBalanceStrategy
    {
        public (List<Player> BlueTeam, List<Player> RedTeam) BalanceTeams(List<Player> players)
        {
            var sortedPlayers = players.OrderByDescending(p => p.Stars).ToList();
            var blueTeam = new List<Player>();
            var redTeam = new List<Player>();

            // Alterna jogadores entre os times para balancear as estrelas
            foreach (var player in sortedPlayers)
            {
                var blueStars = blueTeam.Sum(p => p.Stars);
                var redStars = redTeam.Sum(p => p.Stars);

                // Caso a diferença de estrelas entre os times seja pequena ou se os times estão equilibrados
                if (Math.Abs(blueStars - redStars) <= player.Stars)
                {
                    // Se a diferença for pequena, decidimos por balancear mais
                    if (blueStars < redStars)
                    {
                        blueTeam.Add(player);
                    }
                    else
                    {
                        redTeam.Add(player);
                    }
                }
                else
                {
                    // Se um time está significativamente mais forte, priorizamos o balanceamento
                    if (blueStars <= redStars)
                    {
                        blueTeam.Add(player);
                    }
                    else
                    {
                        redTeam.Add(player);
                    }
                }
            }

            return (blueTeam, redTeam);
        }
    }
}
