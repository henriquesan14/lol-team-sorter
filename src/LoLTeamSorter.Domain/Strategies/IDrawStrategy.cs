using LoLTeamSorter.Domain.Entities;

namespace LoLTeamSorter.Domain.Strategies
{
    public interface IDrawStrategy
    {
        (List<Player> BlueTeam, List<Player> RedTeam) DrawTeams(List<Player> players);
    }
}
