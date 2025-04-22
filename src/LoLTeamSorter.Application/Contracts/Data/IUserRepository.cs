using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Application.Contracts.Data
{
    public interface IUserRepository : IAsyncRepository<User, UserId>
    {
        Task DeleteRange(List<UserId> UserIds);
    }
}
