using LoLTeamSorter.Domain.Exceptions;

namespace LoLTeamSorter.Domain.ValueObjects
{
    public record Username
    {
        public string Value { get; set; }
        private Username(string value) => Value = value;
        public static Username Of(string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (string.IsNullOrEmpty(value))
            {
                throw new DomainException("Username cannot be empty.");
            }
            return new Username(value);
        }
    }
}
