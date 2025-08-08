using Booking1.Application.Authentication;
using Booking1.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClBooking1.Infrastructure.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration configuration;

        public JwtService(IConfiguration config)
        {
            configuration = config;
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,    user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,  user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,    Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name,                $"{user.FirstName} {user.LastName}"),
            };

            var key = configuration["Jwt:Key"]!;
            var issuer = configuration["Jwt:Issuer"]!;
            var audience = configuration["Jwt:Audience"]!;
            var lifetime = TimeSpan.FromMinutes(
               double.Parse(configuration["Jwt:Lifetime"]!));

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(lifetime),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
