using BooksAPI.Dtos;
using MediatR;

namespace BooksAPI.Command.User;

public record UpdateUserCommand(UserDto UserDto, Guid UserId) : IRequest;