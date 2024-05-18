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
        IUserRoleRepository userRoleRepository,
        RoleManager<AppRole> roleManager) : IJwtProvider
    {
        public async Task<string> CreateTokenAsync(AppUser appUser)
        {
            List<AppUserRole> appUserRoles = await userRoleRepository
                .Where(p => p.UserId == appUser.Id).ToListAsync();

            List<AppRole> roles = new();

            foreach(var userRole in appUserRoles)
            {
                AppRole? role = await roleManager.Roles
                    .Where(p => p.Id == userRole.RoleId).FirstOrDefaultAsync();

                if (role is not null)
                {
                    roles.Add(role);
                }
            }

            List<string?> stringRoles = roles.Select(s => s.Name).ToList();

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                new Claim(ClaimTypes.Name, appUser.FullName),
                new Claim(ClaimTypes.Email, appUser.Email ?? string.Empty),
                new Claim("UserName", appUser.UserName ?? string.Empty),
                new Claim(ClaimTypes.Role, JsonSerializer.Serialize(stringRoles))
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
