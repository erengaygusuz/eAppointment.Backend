using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Admins.UpdateAdminProfileById
{
    public sealed record UpdateAdminProfileByIdCommand(
        int id,
        string firstName,
        string lastName,
        string phoneNumber,
        string? profilePhotoPath) : IRequest<Result<string>>;
}
