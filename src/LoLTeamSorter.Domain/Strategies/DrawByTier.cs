using LoLTeamSorter.Domain.Entities;

namespace LoLTeamSorter.Domain.Strategies
{
    public class DrawByTier : IDrawStrategy
    {
        private const int MaxTierDifference = 2; // Diferença máxima permitida entre os times
        private const int MaxAttempts = 10; // Número máximo de tentativas

        public (List<Player> BlueTeam, List<Player> RedTeam) DrawTeams(List<Player> players)
        {
            return TryBalance(players, attempt: 1);
        }

        private (List<Player> BlueTeam, List<Player> RedTeam) TryBalance(List<Player> players, int attempt)
        {
            // Ordena os jogadores por peso do tier (decrescente), depois divide em pares e embaralha dentro de cada par
            var shuffledPlayers = players
                .OrderByDescending(p => p.RankedTier.GetWeight())  // Ordena os jogadores por peso do tier
                .Chunk(2)  // Divide em pares de jogadores
                .SelectMany(chunk => chunk.OrderBy(_ => Guid.NewGuid()))  // Embaralha aleatoriamente dentro de cada par
                .ToList();

            var blueTeam = new List<Player>();
            var redTeam = new List<Player>();

            // Distribui os jogadores nos times com base no balanceamento de peso
            foreach (var player in shuffledPlayers)
            {
                var blueWeight = blueTeam.Sum(p => p.RankedTier.GetWeight());
                var redWeight = redTeam.Sum(p => p.RankedTier.GetWeight());

                // Adiciona o jogador ao time com menor peso
                if (blueWeight <= redWeight)
                    blueTeam.Add(player);
                else
                    redTeam.Add(player);
            }

            // Calcula o peso total dos times
            var blueTotal = blueTeam.Sum(p => p.RankedTier.GetWeight());
            var redTotal = redTeam.Sum(p => p.RankedTier.GetWeight());

            // Verifica se a diferença de peso entre os times é aceitável ou se atingiu o número máximo de tentativas
            if (Math.Abs(blueTotal - redTotal) <= MaxTierDifference || attempt >= MaxAttempts)
            {
                return (blueTeam, redTeam);
            }

            // Caso contrário, tenta balancear de novo recursivamente
            return TryBalance(players, attempt + 1);
        }
    }
}
