using eAppointment.Backend.Domain.Helpers;
using MediatR;

namespace eAppointment.Backend.Application.Features.Users.GetAllUsers
{
    public sealed record GetAllUsersQuery(
        long first,
        long rows,
        string sortField,
        int sortOrder,
        object multiSortMeta,
        Dictionary<string, FilterMetadata[]> filters,
        string globalFilter) : IRequest<Result<GetAllUsersQueryTableResponse>>;
}
