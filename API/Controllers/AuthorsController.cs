using BooksAPI.Command;
using BooksAPI.Command.Edition;
using BooksAPI.Query.Edition;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/authors")]
[Produces("application/json")]
public class AuthorsController : Controller
{
    private readonly IMediator _mediator;

    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var query =await _mediator.Send(new GetEditionsQuery());
        return Ok(query);
    }

    [HttpGet($"{{authorId:Guid}}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(Guid authorId)
    {
        var query = await _mediator.Send(new GetEditionQuery(authorId));
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

    [HttpPut("{author}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(UpdateBookCommand author, Guid id)
    {
        await _mediator.Send(author with { Id = id });
        return NoContent();
    }

    [HttpDelete($@"{{authorId:Guid}}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid authorId)
    {
        await _mediator.Send(new DeleteBookCommand(authorId));
        return NoContent();
    }
}