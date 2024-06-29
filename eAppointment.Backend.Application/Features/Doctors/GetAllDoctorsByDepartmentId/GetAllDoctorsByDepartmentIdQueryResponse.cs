namespace eAppointment.Backend.Application.Features.Doctors.GetAllDoctorsByDepartmentId
{
    public sealed record GetAllDoctorsByDepartmentIdQueryResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
