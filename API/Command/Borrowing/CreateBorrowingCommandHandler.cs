using API.Context.Repository;
using BooksAPI.Exceptions;
using MediatR;

namespace BooksAPI.Command.Borrowing;

public class CreateBorrowingCommandHandler : IRequestHandler<CreateBorrowingCommand>
{
    private readonly IRepository<API.Models.Borrowing> _borrowingRepository;
    private readonly IRepository<API.Models.User> _userRepository;
    private readonly IRepository<API.Models.Edition> _editionRepository;

    public CreateBorrowingCommandHandler(IRepository<API.Models.Borrowing> borrowingRepository,
        IRepository<API.Models.User> userRepository, IRepository<API.Models.Edition> editionRepository)
    {
        _borrowingRepository = borrowingRepository;
        _userRepository = userRepository;
        _editionRepository = editionRepository;
    }

    public async Task Handle(CreateBorrowingCommand request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetAllAsNoTracking()
            .FirstOrDefault(x => x.Id == request.UserId);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        var edition = _editionRepository.GetAllAsNoTracking()
            .FirstOrDefault(x => x.Id == request.EditionId);
        if (edition is null)
        {
            throw new NotFoundException("Edition not found");
        }

        _editionRepository.Attach(edition, user.Department.DataCenter);
        _userRepository.Attach(user, user.Department.DataCenter);

        var borrowing = API.Models.Borrowing.Create(request.BorrowingDto.BorrowDate, request.BorrowingDto.ReturnDate,
            request.BorrowingDto.DueDate,
            request.BorrowingDto.IsReturned, user, edition);

        await _borrowingRepository.AddAsync(borrowing, user.Department.DataCenter);
        await _borrowingRepository.SaveAsync(user.Department.DataCenter);
    }
}