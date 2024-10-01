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
        [HttpGet]
        public async Task<IActionResult> GetAllByCityId([FromQuery(Name = "cityId")] int cityId, CancellationToken cancellationToken)
        {
            var request = new GetAllCountiesByCityIdQuery(cityId);

            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
