using LoLTeamSorter.Domain.Abstractions;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Domain.Entities
{
    public class RefreshToken : Aggregate<RefreshTokenId>
    {
        public string Token { get; set; } = Guid.NewGuid().ToString();
        public UserId UserId { get; private set; } = default!;
        public DateTime ExpiresAt { get; private set; } = default!;
        public bool IsRevoked { get; private set; } = default!;
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

        public virtual User User { get; private set; } = default!;

        public static RefreshToken Create(RefreshTokenId id, string token, UserId userId, DateTime expiresAt)
        {
            return new RefreshToken
            {
                Id = id,
                Token = token,
                UserId = userId,
                ExpiresAt = expiresAt,
                IsRevoked = false
            };
        }

        public void Revoke()
        {
            IsRevoked = true;
        }
    }
}
