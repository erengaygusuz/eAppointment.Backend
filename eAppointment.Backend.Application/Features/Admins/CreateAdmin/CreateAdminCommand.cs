using eAppointment.Backend.Domain.Helpers;
using MediatR;

namespace eAppointment.Backend.Application.Features.Admins.CreateAdmin
{
    public sealed record CreateAdminCommand(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string userName,
        string password) : IRequest<Result<string>>;
}
