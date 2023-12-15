using API.Context.Repository;
using API.Models;
using BooksAPI.Authentication.Jwt;
using BooksAPI.Exceptions;
using MediatR;

namespace BooksAPI.Authentication;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IRepository<User> _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(IRepository<User> userRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);

        var user = _userRepository.GetAllAsNoTracking()
            .FirstOrDefault(x => x.Email == email.Address);

        if (user is null)
        {
            throw new NotFoundException($"User with email {email.Address} not found.");
        }

        var token = _jwtProvider.GenerateToken(user);

        return token;
    }
}