using MediatR;
using eAppointment.Backend.Domain.Helpers;
using Microsoft.AspNetCore.Http;

namespace eAppointment.Backend.Application.Features.Admins.UpdateAdminProfileById
{
    public sealed record UpdateAdminProfileByIdCommand(
        string id,
        string firstName,
        string lastName,
        string phoneNumber,
        IFormFile? profilePhoto) : IRequest<Result<string>>;
}
