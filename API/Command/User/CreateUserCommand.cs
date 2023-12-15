using BooksAPI.Dtos;
using MediatR;

namespace BooksAPI.Command.User;

public record CreateUserCommand(UserDto UserDto, Guid DepartmendId): IRequest;