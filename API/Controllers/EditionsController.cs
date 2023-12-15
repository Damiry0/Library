using BooksAPI.Command;
using BooksAPI.Command.Edition;
using BooksAPI.Query.Edition;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/editions")]
[Produces("application/json")]
public class EditionsController : Controller
{
    private readonly IMediator _mediator;

    public EditionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var query = await _mediator.Send(new GetEditionsQuery());
        return Ok(query);
    }

    [HttpGet($"{{editionId:Guid}}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(Guid editionId)
    {
        var query = await _mediator.Send(new GetEditionQuery(editionId));
        return Ok(query);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateEditionCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{book}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(UpdateBookCommand book, Guid id)
    {
        await _mediator.Send(book with { Id = id });
        return NoContent();
    }

    [HttpDelete($@"{{editionId:Guid}}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid editionId)
    {
        await _mediator.Send(new DeleteBookCommand(editionId));
        return NoContent();
    }
}