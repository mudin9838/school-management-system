using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Enrollments.Commands.EnrollStudent;

namespace SchoolManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,Teacher")]
public class EnrollmentsController : ControllerBase
{
    private readonly IMediator _mediator;
    public EnrollmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Enroll(EnrollStudentCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
