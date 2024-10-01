using eAppointment.Backend.Application.Features.Departments.GetAllDepartments;
using eAppointment.Backend.Domain.Constants;
using eAppointment.Backend.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eAppointment.Backend.WebAPI.Controllers
{
    public sealed class DepartmentsController : ApiController
    {
        public DepartmentsController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Policy = Permissions.GetAllDepartments)]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var request = new GetAllDepartmentsQuery();

            var response = await _mediator.Send(request, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
