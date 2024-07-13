using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Counties.GetAllCountiesByCityId
{
    public sealed class GetAllCountiesByCityIdQueryHandler(
        ICountyRepository countyRepository,
        IMapper mapper) : IRequestHandler<GetAllCountiesByCityIdQuery, Result<List<GetAllCountiesByCityIdQueryResponse>>>
    {
        public async Task<Result<List<GetAllCountiesByCityIdQueryResponse>>> Handle(GetAllCountiesByCityIdQuery request, CancellationToken cancellationToken)
        {
            List<County> counties = await countyRepository
                .Where(x => x.CityId == request.cityId)
                .OrderBy(p => p.Name).ToListAsync(cancellationToken);

            var response = mapper.Map<List<GetAllCountiesByCityIdQueryResponse>>(counties);

            return response;
        }
    }
}
