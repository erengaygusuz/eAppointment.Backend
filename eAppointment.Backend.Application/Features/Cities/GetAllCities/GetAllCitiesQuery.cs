using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Cities.GetAllCities
{
    public sealed record GetAllCitiesQuery() : IRequest<Result<List<GetAllCitiesQueryResponse>>>;
}
