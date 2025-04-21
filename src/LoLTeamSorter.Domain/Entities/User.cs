using LoLTeamSorter.Domain.Abstractions;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Domain.Entities
{
    public class User : Aggregate<UserId>
    {
        public string Name { get; private set; } = default!;
        public Username Username { get; private set; } = default!;
        public string Password { get; private set; } = default!;
        public Group Group { get; private set; } = default!;
        public GroupId GroupId { get; private set; } = default!;

        public static User Create(UserId id, string name, Username username, string password, GroupId groupId)
        {
            return new User { 
                Id = id,
                Name = name,
                Username = username,
                Password = password,
                GroupId = groupId
            };
        }
    }
}
