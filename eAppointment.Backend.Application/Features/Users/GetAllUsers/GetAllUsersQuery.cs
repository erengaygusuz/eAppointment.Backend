using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Users.GetAllUsers
{
    public sealed record GetAllUsersQuery(
        int skip,
        int take,
        string sortFields,
        string sortOrders,
        string searchTerm) : IRequest<Result<GetAllUsersQueryTableResponse>>;
}
