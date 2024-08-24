using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Doctors.GetDoctorById
{
    public sealed record GetDoctorByIdQuery(
        int id) : IRequest<Result<GetDoctorByIdQueryResponse>>;
}
