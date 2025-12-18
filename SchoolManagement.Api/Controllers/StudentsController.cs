using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Students.Commands.CreateStudent;
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

    [HttpPost]
    public async Task<IActionResult> Post(CreateStudentCommand command)
    {
        var studentId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = studentId }, command);
    }
}
