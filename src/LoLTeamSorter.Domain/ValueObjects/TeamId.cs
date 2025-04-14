using LoLTeamSorter.Domain.Exceptions;

namespace LoLTeamSorter.Domain.ValueObjects
{
    public record TeamId
    {
        public Guid Value { get; }
        private TeamId(Guid value) => Value = value;
        public static TeamId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("TeamId cannot be empty.");
            }
            return new TeamId(value);
        }

    }
}
