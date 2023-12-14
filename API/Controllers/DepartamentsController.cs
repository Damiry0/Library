using BooksAPI.Command;
using BooksAPI.Command.Department;
using BooksAPI.Command.Edition;
using BooksAPI.Query.Departament;
using BooksAPI.Query.Edition;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
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
        await _mediator.Send(new GetDepartamentsQuery());
        return Ok();
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