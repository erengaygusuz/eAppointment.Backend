namespace eAppointment.Backend.Application.Features.Doctors.GetDoctorProfileById
{
    public sealed record GetDoctorProfileByIdQueryResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public int DepartmentId { get; set; }
        public string? ProfilePhotoContentType { get; set; }
        public string? ProfilePhotoBase64Content { get; set; }
    }
}
