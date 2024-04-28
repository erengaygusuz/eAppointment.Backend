using eAppointment.Backend.Domain.Entities;

namespace eAppointment.Backend.Application.Services
{
    public interface IJwtProvider
    {
        string CreateToken(AppUser appUser);
    }
}
