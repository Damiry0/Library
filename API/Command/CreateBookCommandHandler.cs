using API.Context;
using API.Models;
using MediatR;

namespace BooksAPI.Command;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand>
{
    private readonly LibraryMsSQLDbContext _context;

    public CreateBookCommandHandler(LibraryMsSQLDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = Book.Create(request.Title, request.PublicationDate, request.Isbn, request.Pages, request.Amount,
            request.Description, new List<Author>(), new List<Edition>());

        await _context.Books.AddAsync(book, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
};