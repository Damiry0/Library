using API.Models;
using MediatR;

namespace BooksAPI.Command;

public record CreateBookCommand(Book Book): IRequest;