using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Infra.Data.Repositories
{
    public class TeamRepository : RepositoryBase<Team, TeamId>, ITeamRepository
    {
        public TeamRepository(LoLTeamSorterDbContext dbContext) : base(dbContext)
        {
        }
    }
}
