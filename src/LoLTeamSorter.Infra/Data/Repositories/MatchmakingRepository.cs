using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LoLTeamSorter.Infra.Data.Repositories
{
    public class MatchmakingRepository : RepositoryBase<Matchmaking, MatchmakingId>, IMatchmakingRepository
    {
        public MatchmakingRepository(LoLTeamSorterDbContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteRange(List<MatchmakingId> MatchmakingIds)
        {
            await DbContext.Matchmakings
            .Where(p => MatchmakingIds.Contains(p.Id))
            .ExecuteDeleteAsync();
        }
    }
}
