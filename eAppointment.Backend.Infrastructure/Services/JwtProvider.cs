﻿using eAppointment.Backend.Application.Services;
using eAppointment.Backend.Domain.Constants;
using eAppointment.Backend.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace eAppointment.Backend.Infrastructure.Services
{
    internal sealed class JwtProvider(
        UserManager<User> userManager) : IJwtProvider
    {
        public async Task<string> CreateTokenAsync(User user)
        {
            var userRole = (await userManager.GetRolesAsync(user))[0];

            var allPermissions = Permissions.GetAllPermissions(userRole);

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
                    new Claim(ClaimTypes.Role, JsonSerializer.Serialize(userRole)),
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
                    new Claim(ClaimTypes.Role, JsonSerializer.Serialize(userRole)),
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
                    new Claim(ClaimTypes.Role, JsonSerializer.Serialize(userRole)),
                    new Claim("Permissions", JsonSerializer.Serialize(allPermissions))
                });
            }

            DateTime expires = DateTime.Now.AddDays(1);

            DotNetEnv.Env.Load();

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Jwt__SecretKey") ?? ""));

            SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);

            JwtSecurityToken jwtSecurityToken = new(
                issuer: Environment.GetEnvironmentVariable("Jwt__Issuer"),
                audience: Environment.GetEnvironmentVariable("Jwt__Audience"),
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
