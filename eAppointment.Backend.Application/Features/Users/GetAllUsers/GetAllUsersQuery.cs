using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.GetAllUsers
{
    public sealed record GetAllUsersQuery() : IRequest<Result<List<GetAllUsersQueryResponse>>>;
}
