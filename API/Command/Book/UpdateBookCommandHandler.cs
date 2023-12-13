using API.Context;
using API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Command;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly LibraryMsSQLDbContext _context;

    public UpdateBookCommandHandler(LibraryMsSQLDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _context.Books.AsTracking().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (book is null)
        {
            throw new Exception("Book cannot be null");
        }

        book = Book.Create(request.Title, request.PublicationDate, request.Isbn, request.Pages, request.Amount,
            request.Description, request.Authors, request.Editions);

        _context.Update(book);
        _context.SaveChangesAsync(cancellationToken);
    }
};