namespace eAppointment.Backend.Application.Features.Roles.GetAllRolesByUserId
{
    public sealed record GetAllRolesByUserIdQueryResponse(
        Guid id,
        string? name);
}
