namespace eAppointment.Backend.Application.Features.Users.GetAllUsers
{
    public sealed record GetAllUsersQueryTableResponse(
        int totalCount, 
        List<GetAllUsersQueryResponse> getAllUsersQueryResponse);
}
