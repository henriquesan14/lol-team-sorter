namespace LoLTeamSorter.Application.Exceptions
{
    public class PlayerAlreadyExistsException : ConflictException
    {
        public PlayerAlreadyExistsException(string key) : base("Player", key)
        {
        }
    }
}
