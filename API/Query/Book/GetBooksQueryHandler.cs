using API.Context;
using BooksAPI.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Query;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<BookDto>>
{
    private readonly LibraryMsSQLDbContext _contextMSSQL;
    private readonly LibraryMySQLDbContext _contextMySQL;

    public GetBooksQueryHandler(LibraryMsSQLDbContext context, LibraryMySQLDbContext contextMySql)
    {
        _contextMSSQL = context;
        _contextMySQL = contextMySql;
    }

    public async Task<IEnumerable<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _contextMSSQL.Books
            .Select(x => new BookDto(x.Title, x.PublicationDate, x.Isbn, x.Pages, x.Amount, x.Description, x.Authors,
                x.Editions))
            .ToListAsync(cancellationToken);

        var booksMySQL = await _contextMySQL.Books
            .Select(x => new BookDto(x.Title, x.PublicationDate, x.Isbn, x.Pages, x.Amount, x.Description, x.Authors,
                x.Editions))
            .ToListAsync(cancellationToken);

        books.AddRange(booksMySQL);
        return books;


        /*await using var transaction = await _contextMSSQL.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var books = await _contextMSSQL.Books
                .Select(x => new BookDto(x.Title, x.PublicationDate, x.Isbn, x.Pages, x.Amount, x.Description, x.Authors,
                    x.Editions))
                .ToListAsync(cancellationToken);
            await using var transactionMySQL = await _contextMySQL.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var booksMySQL = await _contextMySQL.Books
                    .Select(x => new BookDto(x.Title, x.PublicationDate, x.Isbn, x.Pages, x.Amount, x.Description, x.Authors,
                        x.Editions))
                    .ToListAsync(cancellationToken);
                books.AddRange(booksMySQL);
                return books;
            }
            catch
            {
                await transactionMySQL.RollbackAsync(cancellationToken);
            }
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
        }*/
    }
}