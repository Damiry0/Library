using API.Context.Repository;
using API.Models;
using BooksAPI.Dtos;
using MediatR;

namespace BooksAPI.Query.Borrowing;

public class GetBorrowingsQueryHandler : IRequestHandler<GetBorrowingsQuery, IEnumerable<BorrowingDto>>
{
    private readonly IRepository<API.Models.Borrowing> _borrowingRepository;
    private readonly IRepository<API.Models.User> _userRepository;
    private readonly IRepository<API.Models.Edition> _editionRepository;

    public GetBorrowingsQueryHandler(IRepository<API.Models.User> userRepository, IRepository<API.Models.Edition> editionRepository, IRepository<API.Models.Borrowing> borrowingRepository)
    {
        _userRepository = userRepository;
        _editionRepository = editionRepository;
        _borrowingRepository = borrowingRepository;
    }

    public async Task<IEnumerable<BorrowingDto>> Handle(GetBorrowingsQuery request, CancellationToken cancellationToken)
    {
        var users = _borrowingRepository.GetAllAsNoTracking()
            .Select(x => new BorrowingDto(x.BorrowDate, x.ReturnDate, x.DueDate, x.IsReturned)).ToList();

        return users;
    }
}