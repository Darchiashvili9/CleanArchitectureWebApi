using CleanArchitecht.Application.Common.Interfaces.Authentication;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecht.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public string GenerateToken(Guid userId, string firstName, string lastName)
        {
            var signinCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key")),
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName,firstName),
                new Claim(JwtRegisteredClaimNames.FamilyName,lastName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            var securityToken = new JwtSecurityToken(
                issuer: "CleanArchitectWebApp",
                expires: DateTime.Now.AddDays(1),
                claims: claims,
                signingCredentials: signinCredentials);
        
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
