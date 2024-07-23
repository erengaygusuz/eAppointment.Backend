using eAppointment.Backend.Application.Features.Auth.Login;
using eAppointment.Backend.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace eAppointment.Backend.WebAPI.Controllers
{
    [AllowAnonymous]
    public sealed class AuthController : ApiController
    {
        private readonly IStringLocalizer<AuthController> _localization;

        public AuthController(IMediator mediator, IStringLocalizer<AuthController> localization) : base(mediator)
        {
            _localization = localization;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var welcomeMessage = string.Format(_localization["welcome"], name);

            return Ok(welcomeMessage);
        }
    }
}
