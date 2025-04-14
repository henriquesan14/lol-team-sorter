using LoLTeamSorter.Domain.Enums;

namespace LoLTeamSorter.Domain.ValueObjects
{
    public class RankedTier : IEquatable<RankedTier>
    {
        public TierEnum Tier { get; private set; }
        public RankEnum? Rank { get; private set; }

        public RankedTier(TierEnum tier, RankEnum? rank = null)
        {
            Tier = tier;
            Rank = NeedsRank(tier) ? rank ?? throw new ArgumentNullException(nameof(rank)) : null;
        }

        private bool NeedsRank(TierEnum tier)
        {
            return tier is not TierEnum.MASTER || tier is not TierEnum.GRANDMASTER || tier is not TierEnum.CHALLENGER;
        }

        public int GetWeight()
        {
            int tierWeight = (int)Tier * 4;
            int rankWeight = Rank.HasValue ? (5 - (int)Rank.Value) : 0;
            return tierWeight + rankWeight;
        }

        public override string ToString()
            => Rank.HasValue ? $"{Tier} {Rank}" : Tier.ToString();

        // Equals e GetHashCode para comparar corretamente
        public override bool Equals(object? obj) => Equals(obj as RankedTier);

        public bool Equals(RankedTier? other)
        {
            if (other is null) return false;
            return Tier == other.Tier && Rank == other.Rank;
        }

        public override int GetHashCode() => HashCode.Combine(Tier, Rank);
    }
}
