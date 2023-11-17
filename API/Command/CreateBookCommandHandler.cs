using API.Context;
using API.Models;
using MediatR;

namespace BooksAPI.Command;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand>
{
    private readonly ElibraryDbContext _context;
    
    public CreateBookCommandHandler(ElibraryDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        if (request.Book is not null)
        {
            await _context.Books.AddAsync(request.Book, cancellationToken);
        }
        

        return ;
    }
};