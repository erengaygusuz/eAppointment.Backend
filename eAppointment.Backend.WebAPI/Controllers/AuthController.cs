using eAppointment.Backend.Application.Features.Auth.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eAppointment.Backend.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController (IMediator mediator): ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
