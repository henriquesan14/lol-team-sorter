using LoLTeamSorter.Domain.Abstractions;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Domain.Entities
{
    public class Group : Aggregate<GroupId>
    {
        private List<Permission> _permissions = new();

        public string Name { get; private set; } = default!;
        public IReadOnlyCollection<Permission> Permissions => _permissions.AsReadOnly();

        public virtual List<User> Users { get; private set; } = new();

        public static Group Create(GroupId id, string name)
        {
            return new Group
            {
                Id = id,
                Name = name
            };
        }

        public void Update(string name)
        {
            Name = name;
        }

        public void SetPermissions(IEnumerable<Permission> permissoes)
        {
            _permissions.Clear();
            if (permissoes != null)
                _permissions.AddRange(permissoes);
        }

        public void AddPermission(Permission permission) => _permissions.Add(permission);
    }
}
