using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Counties.GetAllCountiesByCityId
{
    public sealed class GetAllCountiesByCityIdQueryHandler(
        ICountyRepository countyRepository,
        IMapper mapper) : IRequestHandler<GetAllCountiesByCityIdQuery, Result<List<GetAllCountiesByCityIdQueryResponse>>>
    {
        public async Task<Result<List<GetAllCountiesByCityIdQueryResponse>>> Handle(GetAllCountiesByCityIdQuery request, CancellationToken cancellationToken)
        {
            List<County> counties = await countyRepository.GetAllAsync(
               expression: x => x.CityId == request.cityId,
               trackChanges: false,
               include: null,
               orderBy: x => x.OrderBy(a => a.Name),
               cancellationToken);

            var response = mapper.Map<List<GetAllCountiesByCityIdQueryResponse>>(counties);

            return response;
        }
    }
}
