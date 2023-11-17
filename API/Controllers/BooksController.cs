using API.Models;
using BooksAPI.Command;
using BooksAPI.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V2;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly Mediator _mediator;

    public BooksController(Mediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        await _mediator.Send(new GetBooksQuery());
        return Ok();
    }

    [HttpGet($"{{bookId:Guid}}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid bookId)
    {
        await _mediator.Send(new GetBookQuery(bookId));
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] Book book)
    {
        await _mediator.Send(new CreateBookCommand(book));
        return NoContent();
    }
    
    [HttpPut("{book}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put([FromBody] Book book)
    {
        await _mediator.Send(new UpdateBookCommand(book));
        return NoContent();
    }

    [HttpDelete($@"{{bookId:Guid}}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid bookId)
    {
        await _mediator.Send(new DeleteBookCommand(bookId));
        return NoContent();
    }
}