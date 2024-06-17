using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.GetDoctorById
{
    public sealed record GetDoctorByIdQuery(
        Guid id) : IRequest<Result<GetDoctorByIdQueryResponse>>;
}
