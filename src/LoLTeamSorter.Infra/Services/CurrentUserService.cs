using LoLTeamSorter.Application.Contracts.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LoLTeamSorter.Infra.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return null;
                }
                return Guid.Parse(userId!);
            }
        }
    }
}
