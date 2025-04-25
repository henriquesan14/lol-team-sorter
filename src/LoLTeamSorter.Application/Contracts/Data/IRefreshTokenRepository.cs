using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;

namespace LoLTeamSorter.Application.Contracts.Data
{
    public interface IRefreshTokenRepository : IAsyncRepository<RefreshToken, RefreshTokenId>
    {
    }
}
