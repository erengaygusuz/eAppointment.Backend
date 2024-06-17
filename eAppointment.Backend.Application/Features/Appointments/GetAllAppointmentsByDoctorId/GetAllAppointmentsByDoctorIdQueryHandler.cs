﻿using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Appointments.GetAllAppointments
{
    internal sealed class GetAllAppointmentsByDoctorIdQueryHandler (
        IAppointmentRepository appointmentRepository): IRequestHandler<GetAllAppointmentsByDoctorIdQuery, Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>>
    {
        public async Task<Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>> Handle(GetAllAppointmentsByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            List<Appointment> appointments = await appointmentRepository
                .Where(p => p.DoctorId == request.doctorId)
                .Include(p => p.Patient)
                .ToListAsync(cancellationToken);

            List<GetAllAppointmentsByDoctorIdQueryResponse> response =
                appointments.Select(s => 
                    new GetAllAppointmentsByDoctorIdQueryResponse
                    (
                        s.Id,
                        s.StartDate,
                        s.EndDate,
                        s.Patient.User.FirstName + " " + s.Patient.User.LastName)
                    ).ToList();

            return response;
        }
    }
}
