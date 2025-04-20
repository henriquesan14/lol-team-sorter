using LoLTeamSorter.Domain.Exceptions;

namespace LoLTeamSorter.Domain.ValueObjects
{
    public record GroupId
    {
        public Guid Value { get; }
        private GroupId(Guid value) => Value = value;
        public static GroupId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("GroupId cannot be empty.");
            }
            return new GroupId(value);
        }
    }
}
