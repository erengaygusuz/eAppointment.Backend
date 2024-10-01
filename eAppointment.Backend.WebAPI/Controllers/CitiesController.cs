using eAppointment.Backend.Application.Features.Cities.GetAllCities;
using eAppointment.Backend.Domain.Constants;
using eAppointment.Backend.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eAppointment.Backend.WebAPI.Controllers
{
    public sealed class CitiesController : ApiController
    {
        public CitiesController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Policy = Permissions.GetAllCities)]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var request = new GetAllCitiesQuery();

            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
