using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Cities.GetAllCities
{
    public sealed class GetAllCitiesQueryHandler(
        ICityRepository cityRepository,
        IMapper mapper) : IRequestHandler<GetAllCitiesQuery, Result<List<GetAllCitiesQueryResponse>>>
    {
        public async Task<Result<List<GetAllCitiesQueryResponse>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            List<City> cities = await cityRepository.GetAll()
                .OrderBy(p => p.Name).ToListAsync(cancellationToken);

            var response = mapper.Map<List<GetAllCitiesQueryResponse>>(cities);

            return response;
        }
    }
}
