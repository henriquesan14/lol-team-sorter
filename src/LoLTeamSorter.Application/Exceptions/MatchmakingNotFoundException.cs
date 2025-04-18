namespace LoLTeamSorter.Application.Exceptions
{
    public class MatchmakingNotFoundException : NotFoundException
    {
        public MatchmakingNotFoundException(Guid id) : base("Matchmaking", id)
        {
        }
    }
}
