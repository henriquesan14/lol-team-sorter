using LoLTeamSorter.Domain.Enums;
using LoLTeamSorter.Domain.Exceptions;

namespace LoLTeamSorter.Domain.ValueObjects
{
    public record RankedTier : IEquatable<RankedTier>
    {
        public TierEnum Tier { get; private set; }
        public RankEnum? Rank { get; private set; }

        private RankedTier(TierEnum tier, RankEnum? rank = null)
        {
            Tier = tier;
            Rank = NeedsRank(tier) ? rank ?? throw new ArgumentNullException(nameof(rank)) : null;
        }
        public static RankedTier Of(string tier, string? rank = null)
        {
            if (!Enum.TryParse<TierEnum>(tier, true, out var tierEnum))
                throw new DomainException($"Invalid tier: {tier}");

            RankEnum? rankEnum = null;

            if (!string.IsNullOrEmpty(rank))
            {
                if (!Enum.TryParse<RankEnum>(rank, true, out var parsedRank))
                    throw new DomainException($"Invalid rank: {rank}");

                rankEnum = parsedRank;
            }

            return new RankedTier(tierEnum, rankEnum);
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
        //public override bool Equals(object? obj) => Equals(obj as RankedTier);

        //public bool Equals(RankedTier? other)
        //{
        //    if (other is null) return false;
        //    return Tier == other.Tier && Rank == other.Rank;
        //}

        public override int GetHashCode() => HashCode.Combine(Tier, Rank);
    }
}
