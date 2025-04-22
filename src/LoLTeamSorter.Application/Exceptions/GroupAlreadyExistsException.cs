namespace LoLTeamSorter.Application.Exceptions
{
    public class GroupAlreadyExistsException : ConflictException
    {
        public GroupAlreadyExistsException(string key) : base("Group", key)
        {
        }
    }
}
