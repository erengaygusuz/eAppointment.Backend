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
        public async Task<IActionResult> Create(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetDoctorById)]
        [HttpPost]
        public async Task<IActionResult> GetById(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetDoctorProfileById)]
        [HttpPost]
        public async Task<IActionResult> GetProfileById(GetDoctorProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetAllDoctorsByDepartmentId)]
        [HttpPost]
        public async Task<IActionResult> GetAllByDepartmentId(GetAllDoctorsByDepartmentIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.UpdateDoctorById)]
        [HttpPost]
        public async Task<IActionResult> UpdateById(UpdateDoctorByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.UpdateDoctorProfileById)]
        [HttpPost]
        public async Task<IActionResult> UpdateProfileById([FromForm] UpdateDoctorProfileByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
