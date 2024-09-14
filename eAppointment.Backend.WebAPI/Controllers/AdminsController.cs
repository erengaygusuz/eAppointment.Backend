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
        public async Task<IActionResult> Create(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetAdminById)]
        [HttpPost]
        public async Task<IActionResult> GetById(GetAdminByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetAdminProfileById)]
        [HttpPost]
        public async Task<IActionResult> GetProfileById(GetAdminProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.UpdateAdminById)]
        [HttpPost]
        public async Task<IActionResult> UpdateById(UpdateAdminByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.UpdateAdminProfileById)]
        [HttpPost]
        public async Task<IActionResult> UpdateProfileById(UpdateAdminProfileByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
