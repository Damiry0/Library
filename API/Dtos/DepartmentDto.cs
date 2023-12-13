using BooksAPI.Enums;

namespace BooksAPI.Dtos;

public record DepartmentDto(string Name, string Address, string Email, string Phone, DataCenter DataCenter);