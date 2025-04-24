using LoLTeamSorter.Domain.Abstractions;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Domain.Entities
{
    public class User : Aggregate<UserId>
    {
        public string Name { get; private set; } = default!;
        public Username Username { get; private set; } = default!;
        public string? Password { get; private set; } = default!;
        public Group Group { get; private set; } = default!;
        public GroupId GroupId { get; private set; } = default!;
        public string? DiscordId { get; private set; } = default!;
        public string? AvatarUrl { get; private set; } = default!;
        public bool LoginExterno { get; private set; } = default;

        public static User Create(UserId id, string name, Username username, string password, GroupId groupId)
        {
            return new User { 
                Id = id,
                Name = name,
                Username = username,
                Password = password,
                GroupId = groupId,
                LoginExterno = false
            };
        }

        public static User CreateExternal(UserId id, string name, Username username, GroupId groupId, string discordId, string avatarUrl)
        {
            return new User
            {
                Id = id,
                Name = name,
                Username = username,
                GroupId = groupId,
                DiscordId = discordId,
                AvatarUrl = avatarUrl,
                LoginExterno = true,
                Password = null
            };
        }

        public void Update(string name, Username username, GroupId groupId)
        {
            Name = name;
            Username = username;
            GroupId = groupId;
        }

        public void UpdatePassword(string password)
        {
            Password = password;
        }
    }
}
