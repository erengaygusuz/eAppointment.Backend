using eAppointment.Backend.Domain.Entities;

namespace eAppointment.Backend.Application.Services
{
    public interface IJwtProvider
    {
        Task<string> CreateTokenAsync(AppUser appUser);
    }
}
