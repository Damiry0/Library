using MediatR;

namespace BooksAPI.Authentication;

public record LoginCommand(string Email) : IRequest<string>;