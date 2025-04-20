using LoLTeamSorter.Domain.Entities;

namespace LoLTeamSorter.Application.Contracts.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
    }
}
