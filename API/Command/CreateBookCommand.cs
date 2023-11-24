using API.Models;
using MediatR;

namespace BooksAPI.Command;

public record CreateBookCommand(string Title, DateTime PublicationDate, string Isbn, int Pages, int Amount,
    string Description, IEnumerable<Author> Authors, IEnumerable<Edition> Editions) : IRequest;