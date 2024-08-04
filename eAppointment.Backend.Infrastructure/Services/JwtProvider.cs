using eAppointment.Backend.Application.Services;
using eAppointment.Backend.Domain.Constants;
using eAppointment.Backend.Domain.Entities;
using Microsoft.AspNetCore.Identity;
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
        UserManager<User> userManager) : IJwtProvider
    {
        public async Task<string> CreateTokenAsync(User user)
        {
            var userRoles = await userManager.GetRolesAsync(user);

            var stringRoles = userRoles!.Select(x => x.ToLower()).ToList();

            var allPermissions = Permissions.GetAllPermissions(userRoles[0]);

            List<Claim> claims = new();

            if (user.Patient != null)
            {
                claims.AddRange(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim("UserName", user.UserName ?? string.Empty),
                    new Claim("PatientId", user.Patient!.Id.ToString()),
                    new Claim(ClaimTypes.Role, JsonSerializer.Serialize(userRoles)),
                    new Claim("Permissions", JsonSerializer.Serialize(allPermissions))
                });
            }

            else if (user.Doctor != null)
            {
                claims.AddRange(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim("UserName", user.UserName ?? string.Empty),
                    new Claim("DoctorId", user.Doctor!.Id.ToString()),
                    new Claim(ClaimTypes.Role, JsonSerializer.Serialize(userRoles)),
                    new Claim("Permissions", JsonSerializer.Serialize(allPermissions))
                });
            }

            else
            {
                claims.AddRange(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim("UserName", user.UserName ?? string.Empty),
                    new Claim(ClaimTypes.Role, JsonSerializer.Serialize(userRoles)),
                    new Claim("Permissions", JsonSerializer.Serialize(allPermissions))
                });
            }

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
