using LoLTeamSorter.Domain.Exceptions;

namespace LoLTeamSorter.Domain.ValueObjects
{
    public record PlayerId
    {
        public Guid Value { get; }
        private PlayerId(Guid value) => Value = value;
        public static PlayerId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("PlayerId cannot be empty.");
            }
            return new PlayerId(value);
        }

    }
}
