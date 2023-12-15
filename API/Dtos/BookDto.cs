using API.Models;

namespace BooksAPI.Dtos;

public record BookDto(string Title, DateTime PublicationDate, string Isbn, int Pages, int Amount,
    string Description);