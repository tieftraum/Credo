using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Credo.Domain.Dtos.Commands;
using Credo.Domain.Interfaces;
using Credo.Domain.Models;
using Credo.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Credo.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IOptions<JwtSettings> jwtIOptions)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtIOptions.Value.Secret));
        }
        public Task<string> CreateToken(User user, int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Name, user.Firstname),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Birthdate, user.DoB.ToString(CultureInfo.InvariantCulture)),
                    new Claim("PersonalNumber", user.PersonalNumber),
                    new Claim("Id", userId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(_key,SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Task.FromResult(tokenHandler.WriteToken(token));
        }
    }
}