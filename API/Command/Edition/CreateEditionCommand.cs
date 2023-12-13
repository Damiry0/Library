using BooksAPI.Dtos;
using MediatR;

namespace BooksAPI.Command.Edition;

public record CreateEditionCommand(Guid BookId, BookDto? BookDto, Guid DepartmentId) : IRequest;