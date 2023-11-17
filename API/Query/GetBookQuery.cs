using BooksAPI.Dtos;
using MediatR;

namespace BooksAPI.Query;

public record GetBookQuery(Guid bookId): IRequest<BookDto>;