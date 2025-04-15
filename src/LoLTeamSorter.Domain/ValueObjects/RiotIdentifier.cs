using LoLTeamSorter.Domain.Exceptions;

namespace LoLTeamSorter.Domain.ValueObjects
{
    public record RiotIdentifier
    {
        public string Name { get; }
        public string Tag { get; }

        private RiotIdentifier(string name, string tag)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name cannot be empty.");

            if (string.IsNullOrWhiteSpace(tag))
                throw new DomainException("Tag cannot be empty.");

            Name = name;
            Tag = tag;
        }

        public static RiotIdentifier Of(string name, string tag) => new(name, tag);

        public override string ToString() => $"{Name}#{Tag}";
    }
}
