using LoLTeamSorter.Domain.Enums;
using LoLTeamSorter.Domain.Exceptions;

namespace LoLTeamSorter.Domain.ValueObjects
{
    public record RankedTier : IEquatable<RankedTier>
    {
        public TierEnum Tier { get; private set; }
        public RankEnum? Rank { get; private set; }

        private static readonly TierEnum[] TiersWithoutRank =
        {
            TierEnum.MASTER,
            TierEnum.GRANDMASTER,
            TierEnum.CHALLENGER,
            TierEnum.UNRANKED
        };

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

        public static RankedTier Unranked()
        => new RankedTier(TierEnum.UNRANKED);

        private bool NeedsRank(TierEnum tier)
        {
            return !TiersWithoutRank.Contains(tier);
        }

        public int GetWeight()
        {
            int tierWeight = (int)Tier * 4;
            int rankWeight = Rank.HasValue ? (5 - (int)Rank.Value) : 0;
            return tierWeight + rankWeight;
        }

        public override string ToString()
            => Rank.HasValue ? $"{Tier} {Rank}" : Tier.ToString();

        public override int GetHashCode() => HashCode.Combine(Tier, Rank);
    }


}
