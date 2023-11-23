using API.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Command;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly ElibraryDbContext _context;
    
    public UpdateBookCommandHandler(ElibraryDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        if (request.Book is not null)
        {
            _context.Books.Update(request.Book);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return ;
    }
    
    
};