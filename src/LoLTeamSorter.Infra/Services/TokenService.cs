using LoLTeamSorter.Application.Contracts.Services;
using LoLTeamSorter.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoLTeamSorter.Infra.Services
{
    public class TokenService(IConfiguration _configuration) : ITokenService
    {
        public string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["TokenSettings:Secret"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.Value.ToString()),
                    new Claim(ClaimTypes.Name, user.Username.Value),
                }),
                NotBefore = DateTime.Now, // Usando horário local
                Expires = DateTime.Now.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var roles = new Claim[] { }.ToList();
            user.Group.Permissions.ToList().ForEach(r =>
            {
                roles.Add(new Claim("Permissions", r.Name));
            });

            tokenDescriptor.Subject.AddClaims(roles);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
