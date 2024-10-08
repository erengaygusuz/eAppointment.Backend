﻿using MediatR;
using eAppointment.Backend.Domain.Helpers;

namespace eAppointment.Backend.Application.Features.Doctors.UpdateDoctorById
{
    public sealed record UpdateDoctorByIdCommand(
        int id,
        string firstName,
        string lastName,
        string phoneNumber,
        string email,
        string userName,
        int departmentId) : IRequest<Result<string>>;
}
