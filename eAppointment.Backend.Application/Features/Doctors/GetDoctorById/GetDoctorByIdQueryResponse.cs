﻿namespace eAppointment.Backend.Application.Features.Doctors.GetDoctorById
{
    public sealed record GetDoctorByIdQueryResponse(
        string firstName,
        string lastName,
        string? email,
        string? phoneNumber,
        string? userName,
        Guid departmentId);
}
