namespace BooksAPI.Dtos;

public record BorrowingDto(DateTime BorrowDate, DateTime? ReturnDate, DateTime DueDate, bool IsReturned);