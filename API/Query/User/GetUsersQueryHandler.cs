using API.Context.Repository;
using API.Models;
using BooksAPI.Dtos;
using MediatR;

namespace BooksAPI.Query.User;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    private readonly IRepository<API.Models.User> _userRepository;
    private readonly IRepository<Department> _departmentRepository;

    public GetUsersQueryHandler(IRepository<API.Models.User> userRepository, IRepository<Department> departmentRepository)
    {
        _userRepository = userRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _userRepository.GetAllAsNoTracking()
            .Select(x => new UserDto(x.FirstName, x.LastName, x.Email, x.StudentNumber)).ToList();

        return users;
    }
}