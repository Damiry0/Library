using API.Context;
using API.Models;
using MediatR;

namespace BooksAPI.Command;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand>
{
    private readonly LibraryDbContext _context;

    public CreateBookCommandHandler(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = Book.Create(request.Title, request.PublicationDate, request.Isbn, request.Pages, request.Amount,
            request.Description, request.Authors, request.Editions);

        await _context.Books.AddAsync(book, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
};