using LoLTeamSorter.Domain.Entities;

namespace LoLTeamSorter.Domain.Strategies
{
    public class BalanceByTier : IBalanceStrategy
    {
        public (List<Player> BlueTeam, List<Player> RedTeam) BalanceTeams(List<Player> players)
        {
            var sortedPlayers = players.OrderByDescending(p => p.RankedTier.GetWeight()).ToList();
            var blueTeam = new List<Player>();
            var redTeam = new List<Player>();

            foreach (var player in sortedPlayers)
            {
                var blueWeight = blueTeam.Sum(p => p.RankedTier.GetWeight());
                var redWeight = redTeam.Sum(p => p.RankedTier.GetWeight());

                // Verifica a diferença de peso do elo entre os times
                if (Math.Abs(blueWeight - redWeight) <= player.RankedTier.GetWeight())
                {
                    // Se a diferença de pesos for pequena, balanceamos de forma mais flexível
                    if (blueWeight < redWeight)
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
                    // Caso um time tenha um peso muito maior, adicionamos o jogador ao time com menos peso
                    if (blueWeight <= redWeight)
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
