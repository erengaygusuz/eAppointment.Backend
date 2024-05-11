using eAppointment.Backend.Application.Features.Appointments.GetAllAppointments;
using eAppointment.Backend.Application.Features.Appointments.GetAllDoctorsByDepartment;
using eAppointment.Backend.Application.Features.Patients.CreatePatient;
using eAppointment.Backend.Application.Features.Patients.DeletePatient;
using eAppointment.Backend.Application.Features.Patients.GetAllPatient;
using eAppointment.Backend.Application.Features.Patients.UpdatePatient;
using eAppointment.Backend.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eAppointment.Backend.WebAPI.Controllers
{
    public sealed class AppointmentsController : ApiController
    {
        public AppointmentsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> GetAllDoctorsByDepartment(GetAllDoctorByDepartmentQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllAppointmentsByDoctorId(GetAllAppointmentsByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
