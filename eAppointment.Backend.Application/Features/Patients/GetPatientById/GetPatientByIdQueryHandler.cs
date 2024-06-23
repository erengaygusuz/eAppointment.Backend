using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.GetPatientById
{
    public sealed class GetPatientByIdQueryHandler(
        IPatientRepository patientRepository,
        IMapper mapper) : IRequestHandler<GetPatientByIdQuery, Result<GetPatientByIdQueryResponse>>
    {
        public async Task<Result<GetPatientByIdQueryResponse>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            Patient? patient = (await patientRepository
                .Where(x => x.UserId == request.id)
                .Include(p => p.User)
                .Include(c => c.County)
                .OrderBy(p => p.User.FirstName).ToListAsync(cancellationToken)).FirstOrDefault();

            var response = mapper.Map<GetPatientByIdQueryResponse>(patient);

            return response;
        }
    }
}
