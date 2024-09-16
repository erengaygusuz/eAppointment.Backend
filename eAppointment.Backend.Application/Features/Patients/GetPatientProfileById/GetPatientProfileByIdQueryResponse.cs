namespace eAppointment.Backend.Application.Features.Patients.GetPatientProfileById
{
    public sealed record GetPatientProfileByIdQueryResponse()
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int CountyId { get; set; }
        public int CityId { get; set; }
        public string FullAddress { get; set; }
        public string? ProfilePhotoContentType { get; set; }
        public string? ProfilePhotoBase64Content { get; set; }
    };
}
