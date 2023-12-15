using BooksAPI.Dtos;
using MediatR;

namespace BooksAPI.Query.Departament;

public record GetDepartamentsQuery(): IRequest<IEnumerable<DepartmentDto>>;