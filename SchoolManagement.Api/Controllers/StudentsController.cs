using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Students.Queries;

namespace SchoolManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _mediator.Send(new GetAllStudentsQuery()));
    }
}
