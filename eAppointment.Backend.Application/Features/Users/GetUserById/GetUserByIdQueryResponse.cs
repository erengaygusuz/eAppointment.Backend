namespace eAppointment.Backend.Application.Features.Users.GetUserById
{
    public sealed record GetUserByIdQueryResponse(
        string fullName,
        string roleName);
}
