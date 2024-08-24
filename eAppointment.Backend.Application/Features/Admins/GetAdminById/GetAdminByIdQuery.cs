using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Admins.GetUserById
{
    public sealed record GetAdminByIdQuery(
        int id) : IRequest<Result<GetAdminByIdQueryResponse>>;
}
