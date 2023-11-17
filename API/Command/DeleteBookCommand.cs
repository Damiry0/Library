
using MediatR;

public record DeleteBookCommand(Guid Id): IRequest;