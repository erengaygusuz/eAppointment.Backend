using eAppointment.Backend.Domain.Entities;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.GetAllDoctor
{
    public sealed record GetAllDoctorQuery() : IRequest<Result<List<Doctor>>>;
}
