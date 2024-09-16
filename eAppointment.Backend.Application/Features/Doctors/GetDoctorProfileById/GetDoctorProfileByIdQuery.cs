using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Doctors.GetDoctorProfileById
{
    public sealed record GetDoctorProfileByIdQuery(
        int id) : IRequest<Result<GetDoctorProfileByIdQueryResponse>>;
}
