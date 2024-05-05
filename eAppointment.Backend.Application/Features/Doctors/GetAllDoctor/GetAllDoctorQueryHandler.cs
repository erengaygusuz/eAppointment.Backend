﻿using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.GetAllDoctor
{
    internal sealed class GetAllDoctorQueryHandler(IDoctorRepository doctorRepository) : IRequestHandler<GetAllDoctorQuery, Result<List<Doctor>>>
    {
        public async Task<Result<List<Doctor>>> Handle(GetAllDoctorQuery request, CancellationToken cancellationToken)
        {
            List<Doctor> doctors = await doctorRepository.GetAll().OrderBy(p => p.Department).ThenBy(p => p.FirstName).ToListAsync(cancellationToken);

            return doctors;
        }
    }
}
