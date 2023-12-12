using API.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Command;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly LibraryMsSQLDbContext _context;

    public DeleteBookCommandHandler(LibraryMsSQLDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _context.Books.Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (book is null)
        {
            throw new Exception("Book cannot be null");
        }

        _context.Remove(book);
        _context.SaveChangesAsync(cancellationToken);
    }
}