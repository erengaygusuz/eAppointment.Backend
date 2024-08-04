using eAppointment.Backend.Application.Features.Counties.GetAllCountiesByCityId;
using eAppointment.Backend.Domain.Constants;
using eAppointment.Backend.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eAppointment.Backend.WebAPI.Controllers
{
    public sealed class CountiesController : ApiController
    {
        public CountiesController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Policy = Permissions.GetAllCountiesByCityId)]
        [HttpPost]
        public async Task<IActionResult> GetAll(GetAllCountiesByCityIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
