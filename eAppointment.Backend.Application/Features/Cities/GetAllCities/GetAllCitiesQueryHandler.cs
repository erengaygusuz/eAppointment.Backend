using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Cities.GetAllCities
{
    public sealed class GetAllCitiesQueryHandler(
        IDepartmentRepository cityRepository) : IRequestHandler<GetAllCitiesQuery, Result<List<GetAllCitiesQueryResponse>>>
    {
        public async Task<Result<List<GetAllCitiesQueryResponse>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            List<Department> citys = await cityRepository.GetAll()
                .OrderBy(p => p.Name).ToListAsync(cancellationToken);

            List<GetAllCitiesQueryResponse> response =
                citys.Select(s =>
                    new GetAllCitiesQueryResponse
                    (
                        s.Id,
                        s.Name
                    )).ToList();

            return response;
        }
    }
}
