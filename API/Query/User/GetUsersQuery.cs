using BooksAPI.Dtos;
using MediatR;

namespace BooksAPI.Query.User;

public record GetUsersQuery(): IRequest<IEnumerable<UserDto>>;