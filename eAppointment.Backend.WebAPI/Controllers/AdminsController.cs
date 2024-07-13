using eAppointment.Backend.Application.Features.Admins.CreateAdmin;
using eAppointment.Backend.Application.Features.Admins.GetUserById;
using eAppointment.Backend.Application.Features.Admins.UpdateAdminById;
using eAppointment.Backend.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eAppointment.Backend.WebAPI.Controllers
{
    public sealed class AdminsController : ApiController
    {
        public AdminsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> GetById(GetAdminByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateById(UpdateAdminByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
