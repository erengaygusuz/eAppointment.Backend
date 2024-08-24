using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Departments.GetAllDepartments
{
    public sealed record GetAllDepartmentsQuery() : IRequest<Result<List<GetAllDepartmentsQueryResponse>>>;
}
