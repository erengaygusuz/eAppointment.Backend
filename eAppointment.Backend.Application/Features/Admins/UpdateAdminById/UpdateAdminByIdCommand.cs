using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Admins.UpdateAdminById
{
    public sealed record UpdateAdminByIdCommand(
        int id,
        string firstName,
        string lastName,
        string phoneNumber,
        string email,
        string userName) : IRequest<Result<string>>;
}
