using API.Context.Repository;
using MediatR;

namespace BooksAPI.Command.User;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IRepository<API.Models.User> _userRepository;

    public UpdateUserCommandHandler(IRepository<API.Models.User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetAll().FirstOrDefault(x => x.Id == request.UserId);
        if( user is null)
        {
            throw new Exception("User not found");
        }
        _userRepository.Attach(user, user.Department.DataCenter);
        user.Update(request.UserDto.FirstName, request.UserDto.LastName, request.UserDto.Email, request.UserDto.StudentNumber);
        await _userRepository.SaveAsync(user.Department.DataCenter);
    }
}