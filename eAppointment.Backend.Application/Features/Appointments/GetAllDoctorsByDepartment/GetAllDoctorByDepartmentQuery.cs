using eAppointment.Backend.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllDoctorsByDepartment
{
    public sealed record GetAllDoctorByDepartmentQuery(int departmentValue) : IRequest<Result<List<Doctor>>>;
}
