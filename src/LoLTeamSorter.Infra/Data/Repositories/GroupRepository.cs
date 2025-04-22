using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LoLTeamSorter.Infra.Data.Repositories
{
    public class GroupRepository : RepositoryBase<Group, GroupId>, IGroupRepository
    {
        public GroupRepository(LoLTeamSorterDbContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteRange(List<GroupId> GroupIds)
        {
            await DbContext.Groups
            .Where(p => GroupIds.Contains(p.Id))
            .ExecuteDeleteAsync();
        }
    }
}
