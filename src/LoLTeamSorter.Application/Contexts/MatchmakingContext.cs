﻿using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.Strategies;

namespace LoLTeamSorter.Application.Contexts
{
    public class MatchmakingContext
    {
        private IBalanceStrategy _balanceStrategy;

        // Definir qual estratégia de balanceamento usar
        public void SetBalanceStrategy(IBalanceStrategy balanceStrategy)
        {
            _balanceStrategy = balanceStrategy;
        }

        // Balancear os times com a estratégia definida
        public (List<Player> BlueTeam, List<Player> RedTeam) BalanceTeams(List<Player> players)
        {
            return _balanceStrategy.BalanceTeams(players);
        }
    }
}
