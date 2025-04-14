using LoLTeamSorter.Domain.Exceptions;

namespace LoLTeamSorter.Domain.ValueObjects
{
    public record MatchmakingId
    {
        public Guid Value { get; }
        private MatchmakingId(Guid value) => Value = value;
        public static MatchmakingId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("MatchmakingId cannot be empty.");
            }
            return new MatchmakingId(value);
        }

    }
}
