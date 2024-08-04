using eAppointment.Backend.Application.Features.Patients.CreatePatient;
using eAppointment.Backend.Application.Features.Patients.GetAllPatientsByDoctorId;
using eAppointment.Backend.Application.Features.Patients.GetPatientById;
using eAppointment.Backend.Application.Features.Patients.UpdatePatientById;
using eAppointment.Backend.Application.Features.Patients.UpdatePatientProfileById;
using eAppointment.Backend.Domain.Constants;
using eAppointment.Backend.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eAppointment.Backend.WebAPI.Controllers
{
    public sealed class PatientsController : ApiController
    {
        public PatientsController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Policy = Permissions.CreatePatient)]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetPatientById)]
        [HttpPost]
        public async Task<IActionResult> GetById(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetAllPatientsByDoctorId)]
        [HttpPost]
        public async Task<IActionResult> GetAllByDoctorId(GetAllPatientsByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.UpdatePatientById)]
        [HttpPost]
        public async Task<IActionResult> UpdateById(UpdatePatientByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.UpdatePatientProfileById)]
        [HttpPost]
        public async Task<IActionResult> UpdateProfileById(UpdatePatientProfileByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
