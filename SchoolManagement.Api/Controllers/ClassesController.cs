using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.Classes.Commands.CreateClass;
using SchoolManagement.Application.Features.Classes.Queries;

namespace SchoolManagement.Api.Controllers;

[Route("api/classes")]
[ApiController]
public class ClassesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClassesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllClassesQuery()));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Post(CreateClassCommand command)
    {
        var classId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id = classId }, command);
    }
}
