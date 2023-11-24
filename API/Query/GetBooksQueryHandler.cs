using API.Context;
using BooksAPI.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Query;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookDto>>
{
    private readonly LibraryDbContext _context;

    public GetBooksQueryHandler(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _context.Books
            .Select(x => new BookDto(x.Title, x.PublicationDate, x.Isbn, x.Pages, x.Amount, x.Description, x.Authors,
                x.Editions))
            .ToListAsync(cancellationToken);

        return books;
    }
}