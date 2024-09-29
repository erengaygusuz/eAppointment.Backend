using eAppointment.Backend.Application.Features.Admins.CreateAdmin;
using eAppointment.Backend.Application.Features.Admins.GetAdminById;
using eAppointment.Backend.Application.Features.Admins.GetAdminProfileById;
using eAppointment.Backend.Application.Features.Admins.UpdateAdminById;
using eAppointment.Backend.Application.Features.Admins.UpdateAdminProfileById;
using eAppointment.Backend.Domain.Constants;
using eAppointment.Backend.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eAppointment.Backend.WebAPI.Controllers
{
    public sealed class AdminsController : ApiController
    {
        public AdminsController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Policy = Permissions.CreateAdmin)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetAdminById)]
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery(Name = "id")] int id, CancellationToken cancellationToken)
        {
            var request = new GetAdminByIdQuery(id);

            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetAdminProfileById)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var request = new GetAdminProfileByIdQuery(id);

            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.UpdateAdminById)]
        [HttpPut]
        public async Task<IActionResult> UpdateById([FromBody] UpdateAdminByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.UpdateAdminProfileById)]
        [HttpPut]
        public async Task<IActionResult> UpdateProfileById([FromForm] UpdateAdminProfileByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
