using BooksAPI.Dtos;
using MediatR;

namespace BooksAPI.Command.Edition;

public record UpdateEditionCommand(Guid Id, EditionDto EditionDto) : IRequest;