using LoLTeamSorter.Domain.Abstractions;
using LoLTeamSorter.Domain.Enums;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Domain.Entities
{
    public class Permission : Aggregate<PermissionId>
    {
        public string Name { get; private set; } = default!;
        public PermissionCategoryEnum PermissionCategory { get; private set; } = default!;

        public virtual List<Group> Groups { get; private set; } = new();
    }
}
