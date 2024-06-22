namespace eAppointment.Backend.Application.Features.Users.GetAllUsers
{
    public sealed record GetAllUsersQueryResponse()
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public List<string>? RoleNames { get; set; }
    };
}
