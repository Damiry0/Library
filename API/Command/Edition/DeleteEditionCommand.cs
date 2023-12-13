using MediatR;

namespace BooksAPI.Command.Edition;

public record DeleteEditionCommand(Guid Id) : IRequest;