namespace LoLTeamSorter.Application.Exceptions
{
    public class GroupNotFoundException : NotFoundException
    {
        public GroupNotFoundException(Guid id) : base("Group", id)
        {
        }
    }
}
