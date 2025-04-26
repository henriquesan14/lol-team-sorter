using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LoLTeamSorter.Infra.Data.Repositories
{
    public class RefreshTokenRepository : RepositoryBase<RefreshToken, RefreshTokenId>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(LoLTeamSorterDbContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteRange(List<RefreshTokenId> RefreshTokenIds)
        {
            await DbContext.RefreshTokens
            .Where(p => RefreshTokenIds.Contains(p.Id))
            .ExecuteDeleteAsync();
        }
    }
}
