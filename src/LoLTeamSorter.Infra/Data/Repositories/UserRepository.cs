using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LoLTeamSorter.Infra.Data.Repositories
{
    public class UserRepository : RepositoryBase<User, UserId>, IUserRepository
    {
        public UserRepository(LoLTeamSorterDbContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteRange(List<UserId> UserIds)
        {
            await DbContext.Users
            .Where(p => UserIds.Contains(p.Id))
            .ExecuteDeleteAsync();
        }
    }
}
