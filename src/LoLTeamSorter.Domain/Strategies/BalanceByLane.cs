using LoLTeamSorter.Domain.Entities;

namespace LoLTeamSorter.Domain.Strategies
{
    public class BalanceByLane : IBalanceStrategy
    {
        public (List<Player> BlueTeam, List<Player> RedTeam) BalanceTeams(List<Player> players)
        {
            // Dividir jogadores por MainLane e SecondaryLane
            var mainLanePlayers = players.GroupBy(p => p.MainLane).ToDictionary(g => g.Key, g => g.ToList());
            var secondaryLanePlayers = players.GroupBy(p => p.SecondaryLane).ToDictionary(g => g.Key, g => g.ToList());

            var blueTeam = new List<Player>();
            var redTeam = new List<Player>();

            // Função para distribuir jogadores entre os times
            void DistributeLanePlayers(string lane, List<Player> lanePlayers)
            {
                int i = 0;
                // A ideia é alternar os jogadores entre os times (sem ultrapassar o número de 5 jogadores por time)
                foreach (var player in lanePlayers)
                {
                    // Alterna os jogadores entre os times para balancear
                    if (blueTeam.Count < 5 && (blueTeam.Sum(p => p.RankedTier.GetWeight()) <= redTeam.Sum(p => p.RankedTier.GetWeight())))
                    {
                        blueTeam.Add(player);
                    }
                    else if (redTeam.Count < 5)
                    {
                        redTeam.Add(player);
                    }
                }
            }

            // Distribuir jogadores das Main Lanes entre os times
            foreach (var lane in mainLanePlayers)
            {
                DistributeLanePlayers(lane.Key.ToString(), lane.Value);
            }

            // Distribuir jogadores das Secondary Lanes entre os times
            foreach (var lane in secondaryLanePlayers)
            {
                DistributeLanePlayers(lane.Key.ToString(), lane.Value);
            }

            // Se algum time tiver menos de 5 jogadores, completar com outros jogadores restantes
            if (blueTeam.Count < 5)
            {
                foreach (var player in players.Where(p => !blueTeam.Contains(p) && blueTeam.Count < 5))
                {
                    blueTeam.Add(player);
                }
            }
            else if (redTeam.Count < 5)
            {
                foreach (var player in players.Where(p => !redTeam.Contains(p) && redTeam.Count < 5))
                {
                    redTeam.Add(player);
                }
            }

            // Retornar os times balanceados
            return (blueTeam, redTeam);
        }
    }
}
