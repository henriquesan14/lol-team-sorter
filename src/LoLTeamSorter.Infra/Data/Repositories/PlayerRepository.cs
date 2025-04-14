using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Infra.Data.Repositories
{
    public class PlayerRepository : RepositoryBase<Player, PlayerId>, IPlayerRepository
    {
        public PlayerRepository(LoLTeamSorterDbContext dbContext) : base(dbContext)
        {
        }
    }
}
