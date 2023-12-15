using BooksAPI.Dtos;
using MediatR;

namespace BooksAPI.Query.Borrowing;

public record GetBorrowingsQuery(): IRequest<IEnumerable<BorrowingDto>>;