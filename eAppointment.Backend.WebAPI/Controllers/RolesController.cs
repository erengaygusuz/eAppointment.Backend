using eAppointment.Backend.Application.Features.Roles.GetAllRoles;
using eAppointment.Backend.Application.Features.Roles.GetMenuItems;
using eAppointment.Backend.Domain.Constants;
using eAppointment.Backend.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eAppointment.Backend.WebAPI.Controllers
{
    public sealed class RolesController : ApiController
    {
        public RolesController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Policy = Permissions.GetAllRoles)]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var request = new GetAllRolesQuery();

            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.GetMenuItems)]
        [HttpGet]
        public async Task<IActionResult> GetMenuItems([FromQuery(Name = "roleName")] string roleName, CancellationToken cancellationToken)
        {
            var request = new GetMenuItemsQuery(roleName);

            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
