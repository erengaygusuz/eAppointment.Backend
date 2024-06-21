using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Cities.GetAllCities
{
    public sealed record GetAllCitiesQuery() : IRequest<Result<List<GetAllCitiesQueryResponse>>>;
}
