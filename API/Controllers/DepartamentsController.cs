using BooksAPI.Command.Department;
using BooksAPI.Query.Departament;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("api/departments")]
[Produces("application/json")]
public class DepartamentsController : Controller
{
    private readonly IMediator _mediator;

    public DepartamentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var query = await _mediator.Send(new GetDepartamentsQuery());
        return Ok(query);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateDepartmentCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}