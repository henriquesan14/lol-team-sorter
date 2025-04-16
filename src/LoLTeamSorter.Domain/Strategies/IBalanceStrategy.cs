using LoLTeamSorter.Domain.Entities;

namespace LoLTeamSorter.Domain.Strategies
{
    public interface IBalanceStrategy
    {
        (List<Player> BlueTeam, List<Player> RedTeam) BalanceTeams(List<Player> players);
    }
}
