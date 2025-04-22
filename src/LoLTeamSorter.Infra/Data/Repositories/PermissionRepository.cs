using LoLTeamSorter.Application.Contracts.Data;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Infra.Data.Repositories
{
    public class PermissionRepository : RepositoryBase<Permission, PermissionId>, IPermissionRepository
    {
        public PermissionRepository(LoLTeamSorterDbContext dbContext) : base(dbContext)
        {
        }
    }
}
