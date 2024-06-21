using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Admins.GetUserById
{
    public sealed record GetAdminByIdQuery(
        Guid id) : IRequest<Result<GetAdminByIdQueryResponse>>;
}
