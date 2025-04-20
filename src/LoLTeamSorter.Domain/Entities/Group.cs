using LoLTeamSorter.Domain.Abstractions;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Domain.Entities
{
    public class Group : Aggregate<GroupId>
    {
        public string Name { get; private set; } = default!;
        public virtual List<Permission> Permissions { get; private set; } = new();

        public virtual List<User> Users { get; private set; } = new();
    }
}
