namespace eAppointment.Backend.Application.Features.Counties.GetAllCountiesByCityId
{
    public sealed record GetAllCountiesByCityIdQueryResponse(
        Guid id,
        string name);
}
