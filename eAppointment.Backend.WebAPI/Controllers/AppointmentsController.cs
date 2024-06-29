using eAppointment.Backend.Application.Features.Appointments.CreateAppointment;
using eAppointment.Backend.Application.Features.Appointments.DeleteAppointmentById;
using eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByDoctorIdAndByStatus;
using eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientId;
using eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientIdByStatus;
using eAppointment.Backend.Application.Features.Appointments.UpdateAppointmentById;
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
        public async Task<IActionResult> Create(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllByDoctorIdAndByStatus(GetAllAppointmentsByDoctorIdAndByStatusQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllByPatientIdAndByStatus(GetAllAppointmentsByPatientIdAndByStatusQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllByPatientId(GetAllAppointmentsByPatientIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAppointmentByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CancelById(CancelAppointmentByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
