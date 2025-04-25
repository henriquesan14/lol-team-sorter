using LoLTeamSorter.Application.Contracts.Services.Response;
using LoLTeamSorter.Domain.Entities;

namespace LoLTeamSorter.Application.Contracts.Services
{
    public interface ITokenService
    {
        AuthTokenResult GenerateAccessToken(User user);
    }
}
