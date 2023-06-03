using CleanArchitecht.Application.Common.Interfaces.Authentication;
using CleanArchitecht.Application.Common.Interfaces.Services;
using CleanArchitecht.Domain.Entities;
using Microsoft.Extensions.Options;
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
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly JwtSettings jwtSettings;

        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
        {
            _dateTimeProvider = dateTimeProvider;
            this.jwtSettings = jwtOptions.Value;
        }

        public string GenerateToken(User user)
        {
            var signinCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName,user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName,user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            var securityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: _dateTimeProvider.UtcNow.AddMinutes(jwtSettings.EpiryMinutes),
                claims: claims,
                signingCredentials: signinCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
