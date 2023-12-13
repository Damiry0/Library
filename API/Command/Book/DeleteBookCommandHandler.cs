using API.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Command;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly LibraryMsSQLDbContext _contextMSSQL;
    private readonly LibraryMySQLDbContext _contextMySQL;

    public DeleteBookCommandHandler(LibraryMsSQLDbContext contextMssql, LibraryMySQLDbContext contextMySql)
    {
        _contextMSSQL = contextMssql;
        _contextMySQL = contextMySql;
    }

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _contextMSSQL.Books.Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (book is null)
        {
            throw new Exception("Book cannot be null");
        }
    }
}