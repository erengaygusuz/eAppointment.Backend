using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Admins.GetAdminProfileById
{
    public sealed record GetAdminProfileByIdQuery(
        int id) : IRequest<Result<GetAdminProfileByIdQueryResponse>>;
}
