using API.Context;
using BooksAPI.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Query;

public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookDto>
{
    private readonly LibraryDbContext _context;

    public GetBookQueryHandler(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<BookDto> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        var book = await _context.Books.Where(x => x.Id == request.bookId)
            .Select(x => new BookDto(x.Title, x.PublicationDate, x.Isbn, x.Pages, x.Amount, x.Description, x.Authors,
                x.Editions))
            .FirstOrDefaultAsync(cancellationToken);

        return book;
    }
}