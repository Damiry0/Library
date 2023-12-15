using BooksAPI.Dtos;
using MediatR;

namespace BooksAPI.Command.Department;

public record CreateDepartmentCommand(DepartmentDto DepartmentDto): IRequest;