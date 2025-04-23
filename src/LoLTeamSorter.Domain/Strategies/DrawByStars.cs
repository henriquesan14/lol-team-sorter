using LoLTeamSorter.Domain.Entities;

namespace LoLTeamSorter.Domain.Strategies
{
    public class DrawByStars : IDrawStrategy
    {
        private const int MaxStarDifference = 2;
        private const int MaxAttempts = 10;
        private const int PlayersPerTeam = 5;  // Definindo a quantidade de jogadores por time

        public (List<Player> BlueTeam, List<Player> RedTeam) DrawTeams(List<Player> players)
        {
            return TryBalance(players, attempt: 1);
        }

        private (List<Player> BlueTeam, List<Player> RedTeam) TryBalance(List<Player> players, int attempt)
        {
            var shuffledPlayers = players
                .OrderByDescending(p => p.Stars)  // Ordena por estrelas para garantir o equilíbrio
                .Chunk(2)  // Divide em "chunks" de 2
                .SelectMany(chunk => chunk.OrderBy(_ => Guid.NewGuid()))  // Embaralha dentro de cada par
                .ToList();

            var blueTeam = new List<Player>();
            var redTeam = new List<Player>();

            foreach (var player in shuffledPlayers)
            {
                var blueStars = blueTeam.Sum(p => p.Stars);
                var redStars = redTeam.Sum(p => p.Stars);

                // Adiciona jogadores ao time com o menor total de estrelas, respeitando a quantidade máxima de jogadores
                if (blueStars <= redStars && blueTeam.Count < PlayersPerTeam)
                    blueTeam.Add(player);
                else if (redTeam.Count < PlayersPerTeam)
                    redTeam.Add(player);
            }

            // Verifica se os times têm a quantidade correta de jogadores
            if (blueTeam.Count == PlayersPerTeam && redTeam.Count == PlayersPerTeam)
            {
                var blueTotal = blueTeam.Sum(p => p.Stars);
                var redTotal = redTeam.Sum(p => p.Stars);

                // Verifica se a diferença de estrelas é aceitável ou se atingiu o número máximo de tentativas
                if (Math.Abs(blueTotal - redTotal) <= MaxStarDifference || attempt >= MaxAttempts)
                {
                    return (blueTeam, redTeam);
                }
            }

            // Caso contrário, tenta balancear de novo recursivamente
            return TryBalance(players, attempt + 1);
        }
    }
}
