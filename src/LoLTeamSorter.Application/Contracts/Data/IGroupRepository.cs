using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Application.Contracts.Data
{
    public interface IGroupRepository : IAsyncRepository<Group, GroupId>
    {
        Task DeleteRange(List<GroupId> UserIds);
    }
}
