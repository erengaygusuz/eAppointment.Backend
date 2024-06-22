using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.GetDoctorById
{
    public sealed record GetDoctorByIdQuery(
        int id) : IRequest<Result<GetDoctorByIdQueryResponse>>;
}
