using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Cities.GetAllCountiesByCityId
{
    public sealed record GetAllCountiesByCityIdQuery(
        Guid cityId) : IRequest<Result<List<GetAllCountiesByCityIdQueryResponse>>>;
}
