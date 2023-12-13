using BooksAPI.Dtos;
using MediatR;

namespace BooksAPI.Command;

public record CreateBookCommand(string Title, DateTime PublicationDate, string Isbn, int Pages, int Amount,
    string Description, IEnumerable<AuthorDto> Authors, IEnumerable<EditionDto> Editions) : IRequest;