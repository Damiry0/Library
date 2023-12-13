using API.Context.Repository;
using API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Command.Edition;

public class CreateEditionCommandHandler : IRequestHandler<CreateEditionCommand>
{
    private readonly IRepository<API.Models.Edition> _editionRepository;
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<Department> _departmentRepository;

    public CreateEditionCommandHandler(IRepository<API.Models.Edition> editionRepository,
        IRepository<Book> bookRepository, IRepository<Department> departmentRepository)
    {
        _editionRepository = editionRepository;
        _bookRepository = bookRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task Handle(CreateEditionCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetAllAsNoTracking().Where(x => x.Id == request.DepartmentId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (department is null)
        {
            throw new Exception("Department cannot be null");
        }

        var book = await _bookRepository.GetAllAsNoTracking().Where(x => x.Id == request.BookId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (book is null)
        {
            book = Book.Create(request.BookDto.Title, request.BookDto.PublicationDate, request.BookDto.Isbn,
                request.BookDto.Pages, request.BookDto.Amount,
                request.BookDto.Description, request.BookDto.Authors, null);
        }

        var edition = API.Models.Edition.Create(book, department, null);

        await _editionRepository.AddAsync(edition, edition.Department.DataCenter);
        await _editionRepository.SaveAsync(edition.Department.DataCenter);
    }
}