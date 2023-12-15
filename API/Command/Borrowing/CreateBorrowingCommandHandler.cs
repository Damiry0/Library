using API.Context.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Command.Borrowing;

public class CreateBorrowingCommandHandler : IRequestHandler<CreateBorrowingCommand>
{
    private readonly IRepository<API.Models.Borrowing> _borrowingRepository;
    private readonly IRepository<API.Models.User> _userRepository;
    private readonly IRepository<API.Models.Edition> _editionRepository;

    public CreateBorrowingCommandHandler(IRepository<API.Models.Borrowing> borrowingRepository, IRepository<API.Models.User> userRepository, IRepository<API.Models.Edition> editionRepository)
    {
        _borrowingRepository = borrowingRepository;
        _userRepository = userRepository;
        _editionRepository = editionRepository;
    }

    public async Task Handle(CreateBorrowingCommand request, CancellationToken cancellationToken)
    {
        // LAZY LOADING ISSUE, MUST BE INCLUDE
        var user = _userRepository.GetAllAsNoTracking().FirstOrDefault
            (x => x.Id == request.UserId);
        if (user is null)
        {
            throw new Exception("User object is null");
        }
        
        var edition = _editionRepository.GetAllAsNoTracking().FirstOrDefault
            (x => x.Id == request.EditionId);
        
        _userRepository.Attach(user, user.Department.DataCenter);

        var borrowing = API.Models.Borrowing.Create(request.BorrowingDto.BorrowDate, request.BorrowingDto.ReturnDate, request.BorrowingDto.DueDate, 
            request.BorrowingDto.IsReturned, user, edition);
        
        if (borrowing is null)
        {
            throw new Exception("Borrowing object is null");
        }
        
        await _borrowingRepository.AddAsync(borrowing, user.Department.DataCenter);
        await _borrowingRepository.SaveAsync(user.Department.DataCenter);
    }
}