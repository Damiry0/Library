using API.Models;
using MediatR;

namespace BooksAPI.Command;

public record UpdateBookCommand(string Title, DateTime PublicationDate, string Isbn, int Pages, int Amount,
    string Description, IEnumerable<Author> Authors, IEnumerable<API.Models.Edition> Editions, Guid Id) : IRequest;