using BooksAPI.Dtos;
using MediatR;

namespace BooksAPI.Query;

public record GetBooksQuery : IRequest<IEnumerable<BookDto>>;