using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Counties.GetAllCountiesByCityId
{
    public sealed record GetAllCountiesByCityIdQuery(
        int cityId) : IRequest<Result<List<GetAllCountiesByCityIdQueryResponse>>>;
}
