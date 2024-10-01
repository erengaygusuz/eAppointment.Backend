using eAppointment.Backend.Application.Features.Users.DeleteUserById;
using eAppointment.Backend.Application.Features.Users.GetAllUsers;
using eAppointment.Backend.Domain.Constants;
using eAppointment.Backend.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eAppointment.Backend.WebAPI.Controllers
{
    public sealed class UsersController : ApiController
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Policy = Permissions.GetAllUsers)]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Policy = Permissions.DeleteUserById)]
        [HttpDelete]
        public async Task<IActionResult> DeleteById([FromQuery(Name = "id")] int id, CancellationToken cancellationToken)
        {
            var request = new DeleteUserByIdCommand(id);

            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
