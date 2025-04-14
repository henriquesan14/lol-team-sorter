using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Infra.Data.Repositories
{
    public class MatchmakingRepository : RepositoryBase<Matchmaking, MatchmakingId>, IMatchmakingRepository
    {
        public MatchmakingRepository(LoLTeamSorterDbContext dbContext) : base(dbContext)
        {
        }
    }
}
