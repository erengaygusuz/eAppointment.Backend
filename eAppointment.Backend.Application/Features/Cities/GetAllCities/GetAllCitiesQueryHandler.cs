using AutoMapper;
using eAppointment.Backend.Domain.Abstractions;
using eAppointment.Backend.Domain.Entities;
using MediatR;
using eAppointment.Backend.Domain.Helpers;
using System.Net;

namespace eAppointment.Backend.Application.Features.Cities.GetAllCities
{
    public sealed class GetAllCitiesQueryHandler(
        ICityRepository cityRepository,
        IMapper mapper) : IRequestHandler<GetAllCitiesQuery, Result<List<GetAllCitiesQueryResponse>>>
    {
        public async Task<Result<List<GetAllCitiesQueryResponse>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            List<City> cities = await cityRepository.GetAllAsync(
                expression: null,
                trackChanges: false,
                include: null,
                orderBy: x => x.OrderBy(a => a.CreatedDate),
                cancellationToken);

            var response = mapper.Map<List<GetAllCitiesQueryResponse>>(cities);

            return Result<List<GetAllCitiesQueryResponse>>.Succeed((int)HttpStatusCode.OK, response);
        }
    }
}
