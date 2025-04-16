using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LoLTeamSorter.Infra.Data.Repositories
{
    public class PlayerRepository : RepositoryBase<Player, PlayerId>, IPlayerRepository
    {
        public PlayerRepository(LoLTeamSorterDbContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteRange(List<PlayerId> PlayerIds)
        {
            await DbContext.Players
            .Where(p => PlayerIds.Contains(p.Id))
            .ExecuteDeleteAsync();
        }
    }
}
