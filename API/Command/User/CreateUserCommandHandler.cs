using API.Context.Repository;
using MediatR;

namespace BooksAPI.Command.User;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IRepository<API.Models.User> _userRepository;
    private readonly IRepository<API.Models.Department> _departmentRepository;

    public CreateUserCommandHandler(IRepository<API.Models.User> userRepository, IRepository<API.Models.Department> departmentRepository)
    {
        _userRepository = userRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var department = _departmentRepository.GetAllAsNoTracking().FirstOrDefault
            (x => x.Id == request.DepartmendId);
        if (department is null)
        {
            throw new Exception("Department object is null");
        }
        _departmentRepository.Attach(department, department.DataCenter);
        var user = API.Models.User.Create(request.UserDto.FirstName, request.UserDto.LastName, request.UserDto.Email, 
            request.UserDto.StudentNumber, department);
        
        await _userRepository.AddAsync(user,department.DataCenter);
        await _userRepository.SaveAsync(department.DataCenter);
    }
}