using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace OrderTaskHandler.Services
{
    public class AuthorizeService(IConfiguration configuration) : IAuthorizeService
    {
        private readonly IConfiguration _configuration = configuration;

        public bool Validate(string username, string password)
        {
            var userSection = _configuration.GetSection($"Users:{username}");
            if (!userSection.Exists())
                return false;

            var expectedPassword = userSection["password"];
            return password == expectedPassword;
        }

        public string? GetRole(string username)
        {
            var userSection = _configuration.GetSection($"Users:{username}");
            return userSection["role"];
        }

        public string GenerateToken(string username, string role)
        {
            var keyString = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("Jwt:Key is not configured.");

            var keyBytes = Encoding.UTF8.GetBytes(keyString);

            var key = new SymmetricSecurityKey(keyBytes);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                notBefore: now,
                expires: now.AddHours(1),
                signingCredentials: creds
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
