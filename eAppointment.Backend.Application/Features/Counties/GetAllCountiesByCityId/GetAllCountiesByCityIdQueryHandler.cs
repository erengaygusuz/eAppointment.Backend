using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Helpers;
using MediatR;
using System.Net;

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

            return Result<List<GetAllCountiesByCityIdQueryResponse>>.Succeed((int)HttpStatusCode.OK, response);
        }
    }
}
