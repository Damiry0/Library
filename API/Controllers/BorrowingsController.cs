using BooksAPI.Command.Borrowing;
using BooksAPI.Query.Borrowing;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/borrowings")]
[Produces("application/json")]
public class BorrowingsController : Controller
{
    private readonly IMediator _mediator;

    public BorrowingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var query = await _mediator.Send(new GetBorrowingsQuery());
        return Ok(query);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateBorrowingCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}