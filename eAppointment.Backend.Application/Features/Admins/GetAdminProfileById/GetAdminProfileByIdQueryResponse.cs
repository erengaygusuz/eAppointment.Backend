namespace eAppointment.Backend.Application.Features.Admins.GetAdminProfileById
{
    public sealed record GetAdminProfileByIdQueryResponse()
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? ProfilePhotoContentType { get; set; }
        public string? ProfilePhotoBase64Content { get; set; }
    };
}
