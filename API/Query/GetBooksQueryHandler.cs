using API.Context;
using BooksAPI.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Query;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookDto>>
{
    private readonly ElibraryDbContext _context;

    public GetBooksQueryHandler(ElibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _context.Books
            .Select(x => new BookDto())
            .ToListAsync(cancellationToken);

        return books;
    }
}