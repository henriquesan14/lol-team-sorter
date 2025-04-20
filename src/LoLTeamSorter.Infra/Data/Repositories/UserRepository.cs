using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Infra.Data.Repositories
{
    public class UserRepository : RepositoryBase<User, UserId>, IUserRepository
    {
        public UserRepository(LoLTeamSorterDbContext dbContext) : base(dbContext)
        {
        }
    }
}
