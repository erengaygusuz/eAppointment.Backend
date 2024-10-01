using eAppointment.Backend.Application.Features.Doctors.CreateDoctor;
using eAppointment.Backend.Application.Features.Doctors.GetAllDoctorsByDepartmentId;
using eAppointment.Backend.Application.Features.Doctors.GetDoctorById;
using eAppointment.Backend.Application.Features.Doctors.GetDoctorProfileById;
using eAppointment.Backend.Application.Features.Doctors.UpdateDoctorById;
using eAppointment.Backend.Application.Features.Doctors.UpdateDoctorProfileById;
using eAppointment.Backend.Domain.Constants;
using eAppointment.Backend.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eAppointment.Backend.WebAPI.Controllers
{
    public sealed class DoctorsController : ApiController
    {
        public DoctorsController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Policy = Permissions.CreateDoctor)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetDoctorById)]
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery(Name = "id")] int id, CancellationToken cancellationToken)
        {
            var request = new GetDoctorByIdQuery(id);

            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetDoctorProfileById)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var request = new GetDoctorProfileByIdQuery(id);

            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetAllDoctorsByDepartmentId)]
        [HttpGet]
        public async Task<IActionResult> GetAllByDepartmentId([FromQuery(Name = "departmentId")] int departmentId, CancellationToken cancellationToken)
        {
            var request = new GetAllDoctorsByDepartmentIdQuery(departmentId);

            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.UpdateDoctorById)]
        [HttpPut]
        public async Task<IActionResult> UpdateById([FromBody] UpdateDoctorByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.UpdateDoctorProfileById)]
        [HttpPut]
        public async Task<IActionResult> UpdateProfileById([FromForm] UpdateDoctorProfileByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
