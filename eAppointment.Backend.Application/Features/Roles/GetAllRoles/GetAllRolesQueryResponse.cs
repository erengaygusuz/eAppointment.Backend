namespace eAppointment.Backend.Application.Features.Roles.GetAllRoles
{
    public sealed record GetAllRolesQueryResponse(
        Guid id,
        string? name);
}
