using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Infra.Data.Repositories
{
    public class GroupRepository : RepositoryBase<Group, GroupId>, IGroupRepository
    {
        public GroupRepository(LoLTeamSorterDbContext dbContext) : base(dbContext)
        {
        }
    }
}
