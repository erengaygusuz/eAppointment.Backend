using eAppointment.Backend.Application.Features.Patients.CreatePatient;
using eAppointment.Backend.Application.Features.Patients.GetAllPatientsByDoctorId;
using eAppointment.Backend.Application.Features.Patients.GetPatientById;
using eAppointment.Backend.Application.Features.Patients.GetPatientProfileById;
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
        public async Task<IActionResult> Create([FromBody] CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetPatientById)]
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery(Name = "id")] int id, CancellationToken cancellationToken)
        {
            var request = new GetPatientByIdQuery(id);

            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetPatientProfileById)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var request = new GetPatientProfileByIdQuery(id);

            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetAllPatientsByDoctorId)]
        [HttpGet]
        public async Task<IActionResult> GetAllByDoctorId([FromQuery(Name = "doctorId")] int doctorId, CancellationToken cancellationToken)
        {
            var request = new GetAllPatientsByDoctorIdQuery(doctorId);

            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.UpdatePatientById)]
        [HttpPut]
        public async Task<IActionResult> UpdateById([FromBody] UpdatePatientByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.UpdatePatientProfileById)]
        [HttpPut]
        public async Task<IActionResult> UpdateProfileById([FromForm] UpdatePatientProfileByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
