using LoLTeamSorter.Domain.Entities;

namespace LoLTeamSorter.Domain.Strategies
{
    public class BalanceRandom : IBalanceStrategy
    {
        public (List<Player> BlueTeam, List<Player> RedTeam) BalanceTeams(List<Player> players)
        {
            var random = new Random();
            var shuffledPlayers = players.OrderBy(p => random.Next()).ToList(); // Embaralha a lista de jogadores
            var blueTeam = new List<Player>();
            var redTeam = new List<Player>();

            for (int i = 0; i < shuffledPlayers.Count; i++)
            {
                if (blueTeam.Count < 5)
                {
                    blueTeam.Add(shuffledPlayers[i]);
                }
                else
                {
                    redTeam.Add(shuffledPlayers[i]);
                }
            }

            return (blueTeam, redTeam);
        }
    }
}
