namespace eAppointment.Backend.Application.Features.Cities.GetAllCountiesByCityId
{
    public sealed record GetAllCountiesByCityIdQueryResponse(
        Guid id,
        string name);
}
