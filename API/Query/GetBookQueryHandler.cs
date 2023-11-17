using API.Context;
using BooksAPI.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Query;

public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookDto>
{
    private readonly ElibraryDbContext _context;
    
    public GetBookQueryHandler(ElibraryDbContext context)
    {
        _context = context;
    }

    public async Task<BookDto> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .Where(x => x.Id == request.bookId)
            .Select(x => new BookDto())
            .FirstOrDefaultAsync(cancellationToken);

        return book;
    }
}