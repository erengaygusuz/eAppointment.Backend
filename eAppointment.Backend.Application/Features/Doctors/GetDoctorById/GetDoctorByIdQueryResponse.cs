namespace eAppointment.Backend.Application.Features.Doctors.GetDoctorById
{
    public sealed record GetDoctorByIdQueryResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public int DepartmentId { get; set; }
    }
}
