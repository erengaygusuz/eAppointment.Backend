using eAppointment.Backend.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Departments.GetAllDepartment
{
    public sealed record GetAllDepartmentsQuery() : IRequest<Result<List<Department>>>;
}
