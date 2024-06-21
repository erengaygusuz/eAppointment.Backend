using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Admins.CreateAdmin
{
    public sealed record CreateAdminCommand(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string userName,
        string password,
        Guid roleId) : IRequest<Result<string>>;
}
