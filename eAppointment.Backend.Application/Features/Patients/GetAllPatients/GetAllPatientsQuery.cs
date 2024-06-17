using eAppointment.Backend.Application.Features.Patients.GetAllPatients;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.GetAllPatient
{
    public sealed record GetAllPatientsQuery() : IRequest<Result<List<GetAllPatientsQueryResponse>>>;
}
