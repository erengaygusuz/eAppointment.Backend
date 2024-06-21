using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.GetUserById
{
    public sealed record GetUserByIdQuery(
        Guid id) : IRequest<Result<GetUserByIdQueryResponse>>;
}
