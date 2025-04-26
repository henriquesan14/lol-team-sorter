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
        public bool ExternalLogin { get; private set; } = default;
        public ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();

        public static User Create(UserId id, string name, Username username, string password, GroupId groupId)
        {
            return new User { 
                Id = id,
                Name = name,
                Username = username,
                Password = password,
                GroupId = groupId,
                ExternalLogin = false
            };
        }

        public static User CreateExternal(UserId id, string name, Username username, GroupId groupId, string discordId)
        {
            return new User
            {
                Id = id,
                Name = name,
                Username = username,
                GroupId = groupId,
                DiscordId = discordId,
                ExternalLogin = true,
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

        public void SetDiscordId(string discordId)
        {
            DiscordId = discordId;
        }

        public void SetAvatarUrl(string avatarUrl)
        {
            AvatarUrl = avatarUrl;
        }

        public void SetExternalLogin(bool externalLogin)
        {
            ExternalLogin = externalLogin;
        }
    }
}
