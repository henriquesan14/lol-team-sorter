using LoLTeamSorter.Application.Contracts.Services;
using LoLTeamSorter.Application.Contracts.Services.Response;
using LoLTeamSorter.Domain.Entities;
using LoLTeamSorter.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoLTeamSorter.Infra.Services
{
    public class TokenService(IConfiguration _configuration) : ITokenService
    {
        public AuthTokenResult GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["TokenSettings:Secret"]!);
            var accessTokenExpiration = DateTime.UtcNow.AddHours(12);
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(7);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.Value.ToString()),
                    new Claim(ClaimTypes.Name, user.Username.Value),
                }),
                NotBefore = DateTime.Now,
                Expires = accessTokenExpiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var roles = new Claim[] { }.ToList();
            user.Group.Permissions.ToList().ForEach(r =>
            {
                roles.Add(new Claim("Permissions", r.Name));
            });

            tokenDescriptor.Subject.AddClaims(roles);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            var refreshToken = Guid.NewGuid().ToString();

            return new AuthTokenResult(accessToken, refreshToken, accessTokenExpiration, refreshTokenExpiration);
        }
    }
}
