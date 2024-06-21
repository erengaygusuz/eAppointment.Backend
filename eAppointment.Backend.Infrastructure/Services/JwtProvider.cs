using eAppointment.Backend.Application.Services;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace eAppointment.Backend.Infrastructure.Services
{
    internal sealed class JwtProvider(
        IConfiguration configuration,
        RoleManager<Role> roleManager) : IJwtProvider
    {
        public async Task<string> CreateTokenAsync(User user)
        {
            Role? role = await roleManager.Roles
                    .Where(p => p.Id == user.RoleId).FirstOrDefaultAsync();

            string stringRole = role!.Name!;

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim("UserName", user.UserName ?? string.Empty),
                new Claim(ClaimTypes.Role, JsonSerializer.Serialize(stringRole))
            };

            DateTime expires = DateTime.Now.AddDays(1);

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"] ?? ""));

            SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);

            JwtSecurityToken jwtSecurityToken = new(
                issuer: configuration.GetSection("Jwt:Issuer").Value,
                audience: configuration.GetSection("Jwt:Audience").Value,
                claims: claims,
                notBefore: DateTime.Now,
                expires: expires,
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler handler = new();

            string token = handler.WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
