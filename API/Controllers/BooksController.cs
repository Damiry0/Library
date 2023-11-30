﻿using BooksAPI.Command;
using BooksAPI.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/books")]
[Produces("application/json")]
public class BooksController : Controller
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
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
    public async Task<IActionResult> Post([FromBody] CreateBookCommand command)
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

    [HttpDelete($@"{{bookId:Guid}}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid bookId)
    {
        await _mediator.Send(new DeleteBookCommand(bookId));
        return NoContent();
    }
}