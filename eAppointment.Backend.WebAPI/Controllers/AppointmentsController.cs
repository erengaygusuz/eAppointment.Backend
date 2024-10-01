using eAppointment.Backend.Application.Features.Appointments.CreateAppointment;
using eAppointment.Backend.Application.Features.Appointments.DeleteAppointmentById;
using eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByDoctorId;
using eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByDoctorIdAndByStatus;
using eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientId;
using eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientIdByStatus;
using eAppointment.Backend.Application.Features.Appointments.UpdateAppointmentById;
using eAppointment.Backend.Application.Features.Appointments.UpdateAppointmentStatusById;
using eAppointment.Backend.Domain.Constants;
using eAppointment.Backend.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eAppointment.Backend.WebAPI.Controllers
{
    public sealed class AppointmentsController : ApiController
    {
        public AppointmentsController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Policy = Permissions.CreateAppointment)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetAllAppointmentsByDoctorId)]
        [HttpGet]
        public async Task<IActionResult> GetAllByDoctorIdAndByStatus([FromQuery(Name = "doctorId")] int doctorId, [FromQuery(Name = "status")] int status, CancellationToken cancellationToken)
        {
            var request = new GetAllAppointmentsByDoctorIdAndByStatusQuery(doctorId, status);

            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetAllAppointmentsByDoctorId)]
        [HttpGet]
        public async Task<IActionResult> GetAllByDoctorId([FromQuery(Name = "doctorId")] int doctorId, CancellationToken cancellationToken)
        {
            var request = new GetAllAppointmentsByDoctorIdQuery(doctorId);

            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetAllAppointmentsByPatientId)]
        [HttpGet]
        public async Task<IActionResult> GetAllByPatientIdAndByStatus([FromQuery(Name = "patientId")] int patientId, [FromQuery(Name = "status")] int status, CancellationToken cancellationToken)
        {
            var request = new GetAllAppointmentsByPatientIdAndByStatusQuery(patientId, status);

            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetAllAppointmentsByPatientId)]
        [HttpGet]
        public async Task<IActionResult> GetAllByPatientId([FromQuery(Name = "patientId")] int patientId, CancellationToken cancellationToken)
        {
            var request = new GetAllAppointmentsByPatientIdQuery(patientId);

            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.UpdateAppointmentById)]
        [HttpPut]
        public async Task<IActionResult> UpdateById([FromBody] UpdateAppointmentByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.UpdateAppointmentStatusById)]
        [HttpPut]
        public async Task<IActionResult> UpdateStatusById([FromBody] UpdateAppointmentStatusByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.CancelAppointmentById)]
        [HttpPut]
        public async Task<IActionResult> CancelById([FromBody] CancelAppointmentByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
